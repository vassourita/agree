import {
  Controller,
  Get,
  Post,
  Param,
  Body,
  UseGuards,
  CacheInterceptor,
  UseInterceptors,
  ClassSerializerInterceptor,
  Query,
  ParseUUIDPipe,
  Put,
  Delete
} from '@nestjs/common'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'

import { CreateServerUseCase } from '../use-cases/create-server/create-server.use-case'
import { DeleteServerUseCase } from '../use-cases/delete-server/delete-server.use-case'
import { FindServerByIdUseCase } from '../use-cases/find-server-by-id/find-server-by-id.use-case'
import { FindServersByOwnerUseCase } from '../use-cases/find-servers-by-owner/find-servers-by-owner.use-case'
import { ListServersUseCase } from '../use-cases/list-servers/list-servers.use-case'
import { UpdateServerUseCase } from '../use-cases/update-server/update-server.use-case'
import { CreateServerDTO } from './dtos/create-server.dto'
import { UpdateServerDTO } from './dtos/update-server.dto'

@Controller('/servers')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
export class ServerController {
  constructor(
    private readonly listServers: ListServersUseCase,
    private readonly findServerById: FindServerByIdUseCase,
    private readonly findServersByOwner: FindServersByOwnerUseCase,
    private readonly createServer: CreateServerUseCase,
    private readonly updateServer: UpdateServerUseCase,
    private readonly deleteServer: DeleteServerUseCase
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard)
  public async index(@Query('page') page: string, @Query('limit') limit: string, @Query('name') name: string) {
    const pagination = {
      page: page ? Number(page) : 1,
      limit: limit ? Number(limit) : 15
    }

    return this.listServers.execute({
      ...pagination,
      query: name
    })
  }

  @Get('/@me')
  @UseGuards(JwtAuthGuard)
  public async me(@CurrentUserId() id: string) {
    return this.findServersByOwner.execute(id)
  }

  @Get('/:id')
  @UseGuards(JwtAuthGuard)
  public async show(@Param('id', new ParseUUIDPipe()) id: string) {
    return this.findServerById.execute(id)
  }

  @Post('/')
  @UseGuards(JwtAuthGuard)
  public async store(@Body() { name }: CreateServerDTO, @CurrentUserId() userId: string) {
    const server = await this.createServer.execute({
      name,
      ownerId: userId
    })

    return server
  }

  @Put('/:id')
  @UseGuards(JwtAuthGuard)
  public async update(@Param('id', new ParseUUIDPipe()) id: string, @Body() { name }: UpdateServerDTO) {
    const server = await this.findServerById.execute(id)

    return this.updateServer.execute({
      server,
      update: { name }
    })
  }

  @Delete('/:id')
  @UseGuards(JwtAuthGuard)
  public async destroy(@Param('id', new ParseUUIDPipe()) serverId: string, @CurrentUserId() userId: string) {
    const server = await this.findServerById.execute(serverId)

    await this.deleteServer.execute({
      server,
      userId
    })
  }
}
