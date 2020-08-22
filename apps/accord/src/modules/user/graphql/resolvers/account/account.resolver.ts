import { UnauthorizedException, UseGuards } from '@nestjs/common'
import { Resolver, Args, Mutation } from '@nestjs/graphql'

import { CreateUserUseCase } from '@modules/user/use-cases/create-user/create-user.use-case'
import { UpdateUserUseCase } from '@modules/user/use-cases/update-user/update-user.use-case'
import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { GqlJwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { AuthProvider } from '@shared/providers/auth.provider'

import { FindUserByEmailUseCase } from '../../../use-cases/find-user-by-email/find-user-by-email.use-case'
import { CreateAccountDTO } from './dtos/create-account.dto'
import { LoginDTO } from './dtos/login.dto'
import { UpdateAccountDTO } from './dtos/update-account.dto'

@Resolver()
export class AccountResolver {
  constructor(
    private readonly createUser: CreateUserUseCase,
    private readonly updateUser: UpdateUserUseCase,
    private readonly auth: AuthProvider,
    private readonly findUserByEmail: FindUserByEmailUseCase
  ) {}

  @Mutation()
  async login(@Args('data') data: LoginDTO) {
    const user = await this.findUserByEmail.execute({ email: data.email })

    if (!(await this.auth.comparePassword(data.password, user.password))) {
      throw new UnauthorizedException('Wrong password')
    }

    const token = await this.auth.signToken(user.id)

    return {
      token
    }
  }

  @Mutation()
  async createAccount(@Args('data') data: CreateAccountDTO) {
    const user = await this.createUser.execute(data)
    const token = await this.auth.signToken(user.id)

    return {
      user,
      token
    }
  }

  @Mutation()
  @UseGuards(GqlJwtAuthGuard)
  async updateAccount(@Args('data') data: UpdateAccountDTO, @CurrentUserId() id: string) {
    const user = await this.updateUser.execute({
      ...data,
      id
    })

    return user
  }
}
