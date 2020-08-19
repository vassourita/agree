import { Module } from '@nestjs/common'

import { UserResolver } from './graphql/resolvers/user.resolver'
@Module({
  providers: [UserResolver]
})
export class UserModule {}
