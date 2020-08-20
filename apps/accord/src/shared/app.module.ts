import { Module } from '@nestjs/common'

import { AccordConfigModule } from '@config/config.module'
import { UserModule } from '@modules/user/user.module'

import { DatabaseModule } from './database/database.module'
import { AccordGraphQLModule } from './graphql/graphql.module'
import { JwtStrategy } from './guards/jwt/jwt.strategy'

@Module({
  imports: [UserModule, DatabaseModule, AccordGraphQLModule, AccordConfigModule],
  providers: [JwtStrategy]
})
export class AppModule {}
