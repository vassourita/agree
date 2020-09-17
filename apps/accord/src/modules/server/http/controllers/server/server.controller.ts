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
  Delete,
  UploadedFile
} from '@nestjs/common'
import { FileInterceptor } from '@nestjs/platform-express'

import { CurrentUserId } from '@shared/guards/jwt/current-user-id.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'

import { ServerOwnerAuthGuard } from '../../../guards/server-owner/server-owner.guard'
import { CreateServerUseCase } from '../../../use-cases/create-server/create-server.use-case'
import { DeleteServerUseCase } from '../../../use-cases/delete-server/delete-server.use-case'
import { FindServerByIdUseCase } from '../../../use-cases/find-server-by-id/find-server-by-id.use-case'
import { FindServersByOwnerUseCase } from '../../../use-cases/find-servers-by-owner/find-servers-by-owner.use-case'
import { ListServersUseCase } from '../../../use-cases/list-servers/list-servers.use-case'
import { UpdateServerUseCase } from '../../../use-cases/update-server/update-server.use-case'
import { ServerIndexDocs } from './docs/server-index.docs'
import { ServerUpdateDocs } from './docs/server-update.docs'
import { ServerDocs } from './docs/server.docs'
import { CreateServerDTO } from './dtos/create-server.dto'

@Controller('/servers')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
@ServerDocs()
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
  @ServerIndexDocs()
  async index(@Query('page') page: string, @Query('limit') limit: string, @Query('name') name: string) {
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
  async me(@CurrentUserId() id: string) {
    return this.findServersByOwner.execute(id)
  }

  @Get('/:server_id')
  @UseGuards(JwtAuthGuard)
  async show(@Param('server_id', new ParseUUIDPipe()) id: string) {
    return this.findServerById.execute(id)
  }

  @Post('/')
  @UseGuards(JwtAuthGuard)
  async store(@Body() { name }: CreateServerDTO, @CurrentUserId() userId: string) {
    const server = await this.createServer.execute({
      name,
      ownerId: userId
    })

    return server
  }

  @Put('/:server_id')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  @UseInterceptors(FileInterceptor('avatar'))
  @ServerUpdateDocs()
  async update(
    @Param('server_id', new ParseUUIDPipe()) id: string,
    @Body('name') name: string,
    @UploadedFile() file: Express.Multer.File
  ) {
    const server = await this.findServerById.execute(id)

    return this.updateServer.execute({
      server,
      update: { name, avatar: file?.filename }
    })
  }

  @Delete('/:server_id')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  async destroy(@Param('server_id', new ParseUUIDPipe()) serverId: string) {
    const server = await this.findServerById.execute(serverId)

    await this.deleteServer.execute({
      server
    })
  }
}
