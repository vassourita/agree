import { Module } from '@nestjs/common'
import { TypeOrmModule } from '@nestjs/typeorm'

import { UserEntity } from './entities/user.entity'
import { UserResolver } from './graphql/resolvers/user.resolver'
import { CreateUserUseCase } from './use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from './use-cases/find-user-by-id/find-user-by-id.use-case'
import { ListUsersUseCase } from './use-cases/list-users/list-users.use-case'

@Module({
  imports: [TypeOrmModule.forFeature([UserEntity])],
  providers: [
    UserResolver,
    CreateUserUseCase,
    ListUsersUseCase,
    FindUserByIdUseCase
  ]
})
export class UserModule {}
