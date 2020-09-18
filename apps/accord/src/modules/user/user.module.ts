import { Module, CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { MulterModule } from '@nestjs/platform-express'
import { TypeOrmModule } from '@nestjs/typeorm'

import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { RedisClientNames } from '@shared/database/redis.service'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'
import redisStore from 'cache-manager-ioredis'

import { UserEntity } from './entities/user.entity'
import { SessionController } from './http/controllers/session/session.controller'
import { UserController } from './http/controllers/user/user.controller'
import { useCases } from './use-cases'

@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity, ChannelEntity]),
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
    }),
    MulterModule.registerAsync({
      useFactory: (config: ConfigService) => ({
        dest: config.get('upload.dir'),
        storage: config.get('upload.storage'),
        fileFilter: config.get('upload.filter')
      }),
      inject: [ConfigService]
    })
  ],
  controllers: [UserController, SessionController],
  providers: [...useCases, AuthProvider, JwtStrategy]
})
export class UserModule {}
