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
import { JwtType } from '@shared/guards/jwt/dtos/jwt-payload.dto'
import { getRepository } from 'typeorm'

import { AddMemberToServerUseCase } from '../../member/add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '../../server/create-server/create-server.use-case'
import { SignInviteTokenUseCase } from './sign-invite-token.use-case'

describe('SignInviteTokenUseCase', () => {
  let sut: SignInviteTokenUseCase

  let jwtService: JwtService
  let createServer: CreateServerUseCase

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
      providers: [SignInviteTokenUseCase, CreateServerUseCase, AddMemberToServerUseCase]
    }).compile()

    sut = moduleRef.get(SignInviteTokenUseCase)
    jwtService = moduleRef.get(JwtService)
    createServer = moduleRef.get(CreateServerUseCase)
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

  it('should sign and return a valid invite token', async () => {
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

    const token = await sut.execute({ serverId: sutServer.id, expiresIn: '7d' })

    const result = jwtService.decode(token)
    const expected = expect.objectContaining({ id: sutServer.id, typ: JwtType.INVITE })
    expect(result).toEqual(expected)
  })
})
