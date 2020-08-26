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
  NotFoundException,
  Delete,
  UnauthorizedException
} from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { Repository } from 'typeorm'

import { ServerMemberEntity } from '../entities/server-member.entity'
import { ServerEntity } from '../entities/server.entity'
import { CreateServerDTO } from './dtos/create-server.dto'
import { UpdateServerDTO } from './dtos/update-server.dto'

@Controller('/servers')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
export class ServerController {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard)
  public async index(@Query('page') page: string, @Query('limit') limit: string, @Query('name') name: string) {
    const pagination = {
      page: page ? Number(page) : 1,
      limit: limit ? Number(limit) : 15
    }

    return this.serverRepository
      .createQueryBuilder('s')
      .offset((pagination.page - 1) * pagination.limit)
      .limit(pagination.limit)
      .where('s.name ILIKE :name', { name: `%${name}%` })
      .orderBy('s.member_count')
      .getMany()
  }

  @Get('/@me')
  @UseGuards(JwtAuthGuard)
  public async me(@CurrentUserId() id: string) {
    return this.serverRepository.find({
      where: { ownerId: id }
    })
  }

  @Get('/:id')
  @UseGuards(JwtAuthGuard)
  public async show(@Param('id', new ParseUUIDPipe()) id: string) {
    const server = await this.serverRepository.findOne(id)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    return server
  }

  @Post('/')
  @UseGuards(JwtAuthGuard)
  public async store(@Body() { name }: CreateServerDTO, @CurrentUserId() userId: string) {
    const server = this.serverRepository.create({
      name,
      ownerId: userId
    })

    server.memberCount = 1

    await this.serverRepository.save(server)

    const serverOwnerAsMember = await this.memberRepository.create({
      memberId: userId,
      serverId: server.id
    })

    await this.memberRepository.save(serverOwnerAsMember)

    return server
  }

  @Put('/:id')
  @UseGuards(JwtAuthGuard)
  public async update(@Param('id', new ParseUUIDPipe()) id: string, @Body() { name }: UpdateServerDTO) {
    const server = await this.serverRepository.findOne(id)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }
    if (server.name !== name) {
      server.name = name
      await this.serverRepository.save(server)
    }

    return server
  }

  @Delete('/:id')
  @UseGuards(JwtAuthGuard)
  public async destroy(@Param('id', new ParseUUIDPipe()) serverId: string, @CurrentUserId() userId: string) {
    const server = await this.serverRepository.findOne(serverId)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    if (server.ownerId !== userId) {
      throw new UnauthorizedException('You might be the server owner to delete the server')
    }

    await this.serverRepository.delete({
      id: serverId
    })
  }
}
