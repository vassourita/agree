import { ConfigService } from '@nestjs/config'
import { JwtModule, JwtService } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { IJwtPayloadDTO, JwtType } from '@shared/guards/jwt/dtos/jwt-payload.dto'
import { getRepository } from 'typeorm'

import { SignInviteTokenUseCase } from '../../invite/sign-invite-token/sign-invite-token.use-case'
import { AddMemberToServerUseCase } from '../../member/add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '../../server/create-server/create-server.use-case'
import { DecodeInviteTokenUseCase } from './decode-invite-token.use-case'

describe('DecodeInviteTokenUseCase', () => {
  let sut: DecodeInviteTokenUseCase

  let signToken: SignInviteTokenUseCase
  let createServer: CreateServerUseCase
  let jwtService: JwtService

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity]),
        JwtModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            secret: config.get('auth.key'),
            signOptions: {
              expiresIn: config.get('auth.jwt.expiresIn'),
              issuer: config.get('auth.jwt.issuer')
            }
          }),
          inject: [ConfigService]
        })
      ],
      providers: [DecodeInviteTokenUseCase, CreateServerUseCase, AddMemberToServerUseCase, SignInviteTokenUseCase]
    }).compile()

    sut = moduleRef.get(DecodeInviteTokenUseCase)
    signToken = moduleRef.get(SignInviteTokenUseCase)
    createServer = moduleRef.get(CreateServerUseCase)
    jwtService = moduleRef.get(JwtService)
  })

  afterAll(async () => {
    await getRepository(UserEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerMemberEntity).createQueryBuilder().delete().execute()
    await getRepository(ChannelEntity).createQueryBuilder().delete().execute()
  })

  it('should be defined', async () => {
    expect(sut).toBeDefined()
    expect(sut.execute).toBeDefined()
  })

  it('should decode an invite token and return the server id', async () => {
    const sutServerOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const sutServer = await createServer.execute({
      name: 'my test server',
      ownerId: sutServerOwner.id
    })

    const token = await signToken.execute({ serverId: sutServer.id, expiresIn: '7d' })

    expect(await sut.execute(token)).toEqual(sutServer.id)
  })

  it('should throw if the token is invalid', async () => {
    await expect(sut.execute('someinvalidtoken123')).rejects.toThrow('Invalid token')
  })

  it('should throw if the token is not of type INVITE', async () => {
    const sutServerOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const sutServer = await createServer.execute({
      name: 'my test server',
      ownerId: sutServerOwner.id
    })

    const payload: IJwtPayloadDTO = {
      id: sutServer.id,
      typ: JwtType.ACCESS
    }
    const token = await jwtService.signAsync(payload, { expiresIn: '7d' })

    await expect(sut.execute(token)).rejects.toThrow('Token is not a invite token')
  })
})
