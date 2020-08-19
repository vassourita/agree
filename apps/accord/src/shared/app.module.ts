import { Module } from '@nestjs/common'
import { ConfigModule, ConfigService } from '@nestjs/config'
import { GraphQLModule } from '@nestjs/graphql'
import { TypeOrmModule } from '@nestjs/typeorm'

import { join } from 'path'
import { UserModule } from 'src/modules/user/user.module'

import { apiConfig } from '../config/api.config'
import { databaseConfig } from '../config/database.config'

@Module({
  imports: [
    UserModule,
    ConfigModule.forRoot({
      isGlobal: true,
      load: [databaseConfig, apiConfig]
    }),
    TypeOrmModule.forRootAsync({
      imports: [ConfigModule],
      useFactory: (config: ConfigService) => ({
        ...config.get(`database.${config.get('api.env')}`)
      }),
      inject: [ConfigService]
    }),
    GraphQLModule.forRoot({
      typePaths: ['../**/*.graphql'],
      definitions: {
        path: join(process.cwd(), '..', '..', 'libs', 'graphql-typedefs', 'graphql.ts')
      }
    })
  ]
})
export class AppModule {}
