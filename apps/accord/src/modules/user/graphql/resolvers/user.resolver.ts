import { Resolver, Query, Args, Mutation } from '@nestjs/graphql'

import { CreateUserInput } from '@agree/graphql-typedefs'

@Resolver('Author')
export class UserResolver {
  @Query()
  async users() {
    return [
      {
        id: 1,
        name: '',
        email: '',
        createdAt: ''
      }
    ]
  }

  @Query()
  async user(@Args('id') id: number) {
    return {
      id,
      name: '',
      email: '',
      createdAt: ''
    }
  }

  @Mutation()
  async createAccount(@Args('data') data: CreateUserInput) {
    return {
      ...data,
      id: 1,
      createdAt: ''
    }
  }
}
