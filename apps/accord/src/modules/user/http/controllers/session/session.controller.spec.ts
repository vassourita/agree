import { CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule, JwtService } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { FindUserByEmailUseCase } from '@modules/user/use-cases/find-user-by-email/find-user-by-email.use-case'
import { FindUserByIdUseCase } from '@modules/user/use-cases/find-user-by-id/find-user-by-id.use-case'
import { DatabaseModule } from '@shared/database/database.module'
import { RedisClientNames } from '@shared/database/redis.service'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'
import redisStore from 'cache-manager-ioredis'

import { SessionController } from './session.controller'

describe('SessionController', () => {
  let sut: SessionController

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
      providers: [AuthProvider, FindUserByEmailUseCase, FindUserByIdUseCase, ConfigService, JwtStrategy],
      controllers: [SessionController]
    }).compile()

    sut = moduleRef.get(SessionController)
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  describe('POST /sessions', () => {
    it('should be defined', () => {
      expect(sut.store).toBeDefined()
    })
  })

  describe('POST /sessions/refresh', () => {
    it('should be defined', () => {
      expect(sut.refresh).toBeDefined()
    })
  })
})
