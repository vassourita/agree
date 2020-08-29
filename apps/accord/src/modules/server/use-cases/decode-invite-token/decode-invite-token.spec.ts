import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { AddMemberToServerUseCase } from '../add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '../create-server/create-server.use-case'
import { SignInviteTokenUseCase } from '../sign-invite-token/sign-invite-token.use-case'
import { DecodeInviteTokenUseCase } from './decode-invite-token.use-case'

describe('DecodeInviteTokenUseCase', () => {
  let sut: DecodeInviteTokenUseCase

  let signToken: SignInviteTokenUseCase
  let createServer: CreateServerUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity]),
        JwtModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            secret: config.get('auth.key'),
            signOptions: {
              expiresIn: config.get('auth.jwt.expiresIn')
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
  })

  afterAll(async () => {
    await getRepository(UserEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerMemberEntity).createQueryBuilder().delete().execute()
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

    const token = await signToken.execute(sutServer.id)

    expect(await sut.execute(token)).toEqual(sutServer.id)
  })

  it('should throw if the token is invalid', async () => {
    await expect(sut.execute('someinvalidtoken123')).rejects.toThrow('Invalid token')
  })
})
