import { Module, CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { MulterModule } from '@nestjs/platform-express'
import { TypeOrmModule } from '@nestjs/typeorm'

import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'
import redisStore from 'cache-manager-ioredis'

import { ServerEntity } from './entities/server.entity'

@Module({
  imports: [
    TypeOrmModule.forFeature([ServerEntity]),
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
  controllers: [],
  providers: [AuthProvider, JwtStrategy]
})
export class ServerModule {}
