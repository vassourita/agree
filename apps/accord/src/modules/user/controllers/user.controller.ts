import { Controller, Get, Post, Put, Param, Body, UseGuards } from '@nestjs/common'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { AuthProvider } from '@shared/providers/auth.provider'

import { ParseNametagPipe } from '../pipes/parse-nametag.pipe'
import { CreateUserUseCase } from '../use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '../use-cases/find-user-by-id/find-user-by-id.use-case'
import { FindUserByNameAndTagUseCase } from '../use-cases/find-user-by-name-and-tag/find-user-by-name-and-tag.use-case'
import { ListUsersUseCase } from '../use-cases/list-users/list-users.use-case'
import { UpdateUserUseCase } from '../use-cases/update-user/update-user.use-case'
import { CreateAccountDTO } from './dtos/create-account.dto'
import { UpdateAccountDTO } from './dtos/update-account.dto'

@Controller('/users')
export class UserController {
  constructor(
    private readonly auth: AuthProvider,
    private readonly createUser: CreateUserUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly findUserByNameAndTag: FindUserByNameAndTagUseCase,
    private readonly listUsers: ListUsersUseCase,
    private readonly updateUser: UpdateUserUseCase
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard)
  public async index() {
    return this.listUsers.execute()
  }

  @Get('/@me')
  @UseGuards(JwtAuthGuard)
  public async me(@CurrentUserId() id: string) {
    return this.findUserById.execute({ id })
  }

  @Get('/:nametag')
  @UseGuards(JwtAuthGuard)
  public async show(@Param('nametag', new ParseNametagPipe()) [name, tag]: [string, number]) {
    return this.findUserByNameAndTag.execute({ name, tag })
  }

  @Post('/')
  @UseGuards(JwtAuthGuard)
  public async store(@Body() data: CreateAccountDTO) {
    const user = await this.createUser.execute(data)
    const token = await this.auth.signToken(user.id)

    return {
      user,
      token
    }
  }

  @Put('/')
  @UseGuards(JwtAuthGuard)
  public async update(@Body() data: UpdateAccountDTO, @CurrentUserId() id: string) {
    const user = await this.updateUser.execute({
      ...data,
      id
    })

    return user
  }
}
