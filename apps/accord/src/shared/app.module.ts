import { Module } from '@nestjs/common'
import { ConfigModule, ConfigService } from '@nestjs/config'
import { GraphQLModule } from '@nestjs/graphql'
import { TypeOrmModule } from '@nestjs/typeorm'

import { join } from 'path'

import { apiConfig } from '../config/api.config'
import { databaseConfig } from '../config/database.config'

@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      load: [databaseConfig, apiConfig]
    }),
    TypeOrmModule.forRootAsync({
      imports: [ConfigModule],
      useFactory: (config: ConfigService) => {
        const env = config.get('api.env')
        const { host, port, username, password, database } = config.get(
          `database.postgres.${env}`
        )
        return {
          type: 'postgres',
          host,
          port,
          username,
          password,
          database
        }
      },
      inject: [ConfigService]
    }),
    GraphQLModule.forRoot({
      typePaths: ['../**/*.graphql'],
      definitions: {
        path: join(
          process.cwd(),
          '..',
          '..',
          'libs',
          'graphql-typedefs',
          'graphql.ts'
        )
      }
    })
  ]
})
export class AppModule {}
