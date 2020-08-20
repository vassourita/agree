import { UseGuards, ParseUUIDPipe } from '@nestjs/common'
import { Resolver, Query, Args } from '@nestjs/graphql'

import { ParseNametagPipe } from '@modules/user/pipes/parse-nametag.pipe'
import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { GqlJwtAuthGuard } from '@shared/guards/jwt/jwt.guard'

import { FindUserByIdUseCase } from '../../use-cases/find-user-by-id/find-user-by-id.use-case'
import { FindUserByNameAndTagUseCase } from '../../use-cases/find-user-by-name-and-tag/find-user-by-name-and-tag.use-case'
import { ListUsersUseCase } from '../../use-cases/list-users/list-users.use-case'

@Resolver()
export class UserResolver {
  constructor(
    private readonly listUsers: ListUsersUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly findUserByNameAndTag: FindUserByNameAndTagUseCase
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
  async userById(
    @Args('id', new ParseUUIDPipe())
    id: string
  ) {
    return this.findUserById.execute({ id })
  }

  @Query()
  @UseGuards(GqlJwtAuthGuard)
  async userByNameTag(
    @Args('nameTag', new ParseNametagPipe())
    [name, tag]: [string, number]
  ) {
    return this.findUserByNameAndTag.execute({
      name,
      tag
    })
  }
}
