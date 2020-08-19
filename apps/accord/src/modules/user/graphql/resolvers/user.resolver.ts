import { CreateUserInput } from '@agree/graphql-typedefs'

import { UseGuards } from '@nestjs/common'
import { Resolver, Query, Args, Mutation } from '@nestjs/graphql'

import { CurrentUserId } from 'src/shared/guards/jwt/jwt-autheticated-user.decorator'
import { GqlJwtAuthGuard } from 'src/shared/guards/jwt/jwt.guard'

import { CreateUserUseCase } from '../../use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '../../use-cases/find-user-by-id/find-user-by-id.use-case'
import { ListUsersUseCase } from '../../use-cases/list-users/list-users.use-case'

@Resolver('Author')
export class UserResolver {
  constructor(
    private readonly createUser: CreateUserUseCase,
    private readonly listUsers: ListUsersUseCase,
    private readonly findUserById: FindUserByIdUseCase
  ) {}

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async me(@CurrentUserId() id: string) {
    return this.findUserById.execute(id)
  }

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async users() {
    return this.listUsers.execute()
  }

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async user(@Args('id') id: string) {
    return this.findUserById.execute(id)
  }

  @Mutation()
  async createAccount(@Args('data') data: CreateUserInput) {
    return this.createUser.execute(data)
  }
}
