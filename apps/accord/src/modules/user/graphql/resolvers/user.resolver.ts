import { CreateUserInput } from '@agree/graphql-typedefs'

import { UseGuards, ParseUUIDPipe } from '@nestjs/common'
import { Resolver, Query, Args, Mutation } from '@nestjs/graphql'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { GqlJwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { AuthProvider } from '@shared/providers/auth.provider'

import { CreateUserUseCase } from '../../use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '../../use-cases/find-user-by-id/find-user-by-id.use-case'
import { FindUserByNameAndTagUseCase } from '../../use-cases/find-user-by-name-and-tag/find-user-by-name-and-tag.use-case'
import { ListUsersUseCase } from '../../use-cases/list-users/list-users.use-case'

@Resolver()
export class UserResolver {
  constructor(
    private readonly createUser: CreateUserUseCase,
    private readonly listUsers: ListUsersUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly findUserByNameAndTag: FindUserByNameAndTagUseCase,
    private readonly auth: AuthProvider
  ) {}

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async me(@CurrentUserId() id: string) {
    return this.findUserById.execute({ id })
  }

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async users() {
    return this.listUsers.execute()
  }

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async user(
    @Args('id', new ParseUUIDPipe())
    id: string
  ) {
    return this.findUserById.execute({ id })
  }

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async userByNameTag(
    @Args('nameTag')
    nameTag: string
  ) {
    const [name, tag] = nameTag.split('#')
    return this.findUserByNameAndTag.execute({
      name,
      tag: Number(tag)
    })
  }

  @Mutation()
  async createAccount(@Args('data') data: CreateUserInput) {
    const user = await this.createUser.execute(data)
    const token = await this.auth.signToken(user.id)

    return {
      user,
      token
    }
  }
}
