import { Module } from '@nestjs/common'
import { GraphQLModule } from '@nestjs/graphql'

import { join } from 'path'

@Module({
  imports: [
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
