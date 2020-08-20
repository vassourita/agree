import { LoginInput } from '@agree/graphql-typedefs'

import { UnauthorizedException } from '@nestjs/common'
import { Resolver, Args, Mutation } from '@nestjs/graphql'

import { AuthProvider } from '@shared/providers/auth.provider'

import { FindUserByEmailUseCase } from '../../use-cases/find-user-by-email/find-user-by-email.use-case'

@Resolver()
export class AuthResolver {
  constructor(private readonly auth: AuthProvider, private readonly findUserByEmail: FindUserByEmailUseCase) {}

  @Mutation()
  async login(@Args('data') data: LoginInput) {
    const user = await this.findUserByEmail.execute({ email: data.email })

    if (!(await this.auth.comparePassword(data.password, user.password))) {
      throw new UnauthorizedException('Wrong password')
    }

    const token = await this.auth.signToken(user.id)

    return {
      token
    }
  }
}
