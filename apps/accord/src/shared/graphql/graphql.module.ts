import { Module } from '@nestjs/common'
import { GraphQLModule } from '@nestjs/graphql'

import { join } from 'path'

import { UploadScalar } from './upload.scalar'

@Module({
  imports: [
    GraphQLModule.forRoot({
      typePaths: [join(process.cwd(), '/**/*.graphql')],
      definitions: {
        path: join(process.cwd(), '..', '..', 'libs', 'graphql-typedefs', 'graphql.ts')
      },
      uploads: {
        maxFileSize: 4000000, // 4 MB
        maxFiles: 5
      },
      context: ({ req }) => ({ req })
    })
  ],
  providers: [UploadScalar]
})
export class AccordGraphQLModule {}
