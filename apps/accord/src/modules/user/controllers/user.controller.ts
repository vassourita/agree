import {
  Controller,
  Get,
  Post,
  Put,
  Param,
  Body,
  UseGuards,
  CacheInterceptor,
  UseInterceptors,
  UploadedFile,
  ClassSerializerInterceptor,
  Query
} from '@nestjs/common'
import { FileInterceptor } from '@nestjs/platform-express'
import { ApiTags, ApiBearerAuth, ApiConsumes, ApiParam, ApiQuery } from '@nestjs/swagger'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'

import { ParseNametagPipe } from '../pipes/parse-nametag.pipe'
import { CreateUserUseCase } from '../use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '../use-cases/find-user-by-id/find-user-by-id.use-case'
import { FindUserByNameAndTagUseCase } from '../use-cases/find-user-by-name-and-tag/find-user-by-name-and-tag.use-case'
import { ListUsersUseCase } from '../use-cases/list-users/list-users.use-case'
import { UpdateUserUseCase } from '../use-cases/update-user/update-user.use-case'
import { CreateAccountDTO } from './dtos/create-account.dto'
import { UpdateAccountDTO } from './dtos/update-account.dto'

@Controller('/users')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
@ApiTags('users')
export class UserController {
  constructor(
    private readonly createUser: CreateUserUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly findUserByNameAndTag: FindUserByNameAndTagUseCase,
    private readonly listUsers: ListUsersUseCase,
    private readonly updateUser: UpdateUserUseCase
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth()
  @ApiQuery({ name: 'page', required: false, type: 'integer' })
  @ApiQuery({ name: 'limit', required: false, type: 'integer' })
  async index(@Query('page') page?: string, @Query('limit') limit?: string) {
    const pagination = {
      page: page && Number(page),
      limit: limit && Number(limit)
    }
    return this.listUsers.execute(pagination)
  }

  @Get('/@me')
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth()
  async me(@CurrentUserId() id: string) {
    return this.findUserById.execute({ id })
  }

  @Get('/:nametag')
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth()
  @ApiParam({ name: 'nametag', example: 'Vassoura#8230', type: 'string' })
  async show(@Param('nametag', new ParseNametagPipe()) [name, tag]: [string, number]) {
    return this.findUserByNameAndTag.execute({ name, tag })
  }

  @Post('/')
  async store(@Body() data: CreateAccountDTO) {
    const user = await this.createUser.execute(data)

    return user
  }

  @Put('/')
  @UseGuards(JwtAuthGuard)
  @UseInterceptors(FileInterceptor('avatar'))
  @ApiBearerAuth()
  @ApiConsumes('multipart/form-data')
  async update(@Body() data: UpdateAccountDTO, @UploadedFile() file: Express.Multer.File, @CurrentUserId() id: string) {
    const user = await this.updateUser.execute({
      id,
      name: data.name,
      status: data.status,
      avatar: file.filename
    })

    return user
  }
}
