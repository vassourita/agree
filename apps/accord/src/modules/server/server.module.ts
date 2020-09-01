import { Module, CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { MulterModule } from '@nestjs/platform-express'
import { TypeOrmModule } from '@nestjs/typeorm'

import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'
import redisStore from 'cache-manager-ioredis'

import { InviteController } from './controllers/invite.controller'
import { MemberController } from './controllers/member.controller'
import { ServerController } from './controllers/server.controller'
import { ServerMemberEntity } from './entities/server-member.entity'
import { ServerEntity } from './entities/server.entity'
import { useCases } from './use-cases'

@Module({
  imports: [
    TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity]),
    JwtModule.registerAsync({
      useFactory: (config: ConfigService) => ({
        secret: config.get('auth.key'),
        signOptions: {
          expiresIn: config.get('auth.jwt.expiresIn')
        }
      }),
      inject: [ConfigService]
    }),
    CacheModule.registerAsync({
      useFactory: (config: ConfigService) => ({
        store: redisStore,
        host: config.get('database.redis.host'),
        port: config.get('database.redis.port'),
        password: config.get('database.redis.password')
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
  controllers: [ServerController, MemberController, InviteController],
  providers: [...useCases, AuthProvider, JwtStrategy]
})
export class ServerModule {}
