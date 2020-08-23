import { Controller, Post, Body, UnauthorizedException } from '@nestjs/common'

import { AuthProvider } from '@shared/providers/auth.provider'

import { FindUserByEmailUseCase } from '../use-cases/find-user-by-email/find-user-by-email.use-case'
import { CreateAccountDTO } from './dtos/create-account.dto'

@Controller('/sessions')
export class SessionController {
  constructor(private readonly auth: AuthProvider, private readonly findUserByEmail: FindUserByEmailUseCase) {}

  @Post('/')
  public async store(@Body() data: CreateAccountDTO) {
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
