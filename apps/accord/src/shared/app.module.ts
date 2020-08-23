import { Module, CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { ServeStaticModule } from '@nestjs/serve-static'

import { AccordConfigModule } from '@config/config.module'
import { UserModule } from '@modules/user/user.module'
import redisStore from 'cache-manager-ioredis'

import { DatabaseModule } from './database/database.module'
import { AccordGraphQLModule } from './graphql/graphql.module'
import { JwtStrategy } from './guards/jwt/jwt.strategy'

@Module({
  imports: [
    UserModule,
    DatabaseModule,
    AccordGraphQLModule,
    AccordConfigModule,
    ServeStaticModule.forRootAsync({
      useFactory: (config: ConfigService) => [
        {
          rootPath: config.get('upload.dir'),
          serveRoot: '/files'
        }
      ],
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
    })
  ],
  providers: [JwtStrategy]
})
export class AppModule {}
