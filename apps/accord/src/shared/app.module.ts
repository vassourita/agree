import { Module } from '@nestjs/common'
import { ConfigModule, ConfigService } from '@nestjs/config'
import { GraphQLModule } from '@nestjs/graphql'
import { TypeOrmModule } from '@nestjs/typeorm'

import { join } from 'path'
import { UserModule } from 'src/modules/user/user.module'

import { apiConfig } from '../config/api.config'
import { authConfig } from '../config/auth.config'
import { databaseConfig } from '../config/database.config'
import { JwtStrategy } from './guards/jwt/jwt.strategy'

@Module({
  imports: [
    UserModule,
    ConfigModule.forRoot({
      isGlobal: true,
      load: [databaseConfig, apiConfig, authConfig]
    }),
    TypeOrmModule.forRootAsync({
      imports: [ConfigModule],
      useFactory: (config: ConfigService) => config.get(`database.${config.get('api.env')}`),
      inject: [ConfigService]
    }),
    GraphQLModule.forRoot({
      typePaths: ['../**/*.graphql'],
      definitions: {
        path: join(process.cwd(), '..', '..', 'libs', 'graphql-typedefs', 'graphql.ts')
      },
      context: ({ req }) => ({ req })
    })
  ],
  providers: [JwtStrategy]
})
export class AppModule {}
