import { CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { DecodeInviteTokenUseCase } from '@modules/server/use-cases/invite/decode-invite-token/decode-invite-token.use-case'
import { SignInviteTokenUseCase } from '@modules/server/use-cases/invite/sign-invite-token/sign-invite-token.use-case'
import { AddMemberToServerUseCase } from '@modules/server/use-cases/member/add-member-to-server/add-member-to-server.use-case'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { RedisClientNames } from '@shared/database/redis.service'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import redisStore from 'cache-manager-ioredis'

import { FindServerByIdUseCase } from '../../../use-cases/server/find-server-by-id/find-server-by-id.use-case'
import { ChannelController } from './channel.controller'

describe('ChannelController', () => {
  let sut: ChannelController

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
        }),
        CacheModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            store: redisStore,
            host: config.get('database.redis.host'),
            port: config.get('database.redis.port'),
            password: config.get('database.redis.password'),
            ttl: config.get('database.redis.ttl'),
            keyPrefix: RedisClientNames.CACHE
          }),
          inject: [ConfigService]
        })
      ],
      providers: [
        SignInviteTokenUseCase,
        DecodeInviteTokenUseCase,
        AddMemberToServerUseCase,
        FindServerByIdUseCase,
        JwtStrategy
      ],
      controllers: [ChannelController]
    }).compile()

    sut = moduleRef.get(ChannelController)
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  describe('GET /servers/:server_id/channels', () => {
    it('should be defined', () => {
      expect(sut.index).toBeDefined()
    })
  })

  describe('POST /servers/:server_id/channels', () => {
    it('should be defined', () => {
      expect(sut.store).toBeDefined()
    })
  })

  describe('PUT /servers/:server_id/channels/:channel_id', () => {
    it('should be defined', () => {
      expect(sut.destroy).toBeDefined()
    })
  })

  describe('DELETE /servers/:server_id/channels/:channel_id', () => {
    it('should be defined', () => {
      expect(sut.destroy).toBeDefined()
    })
  })
})
