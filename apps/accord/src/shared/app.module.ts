import { Module } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { ServeStaticModule } from '@nestjs/serve-static'

import { AccordConfigModule } from '@config/config.module'
import { ChannelModule } from '@modules/channel/channel.module'
import { ServerModule } from '@modules/server/server.module'
import { UserModule } from '@modules/user/user.module'

import { DatabaseModule } from './database/database.module'
import { JwtStrategy } from './guards/jwt/jwt.strategy'

@Module({
  imports: [
    UserModule,
    ServerModule,
    ChannelModule,
    DatabaseModule,
    AccordConfigModule,
    ServeStaticModule.forRootAsync({
      useFactory: (config: ConfigService) => [
        {
          rootPath: config.get('upload.dir'),
          serveRoot: '/files'
        }
      ],
      inject: [ConfigService]
    })
  ],
  providers: [JwtStrategy]
})
export class AppModule {}
