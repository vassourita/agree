import {
  Controller,
  Get,
  Post,
  Param,
  Delete,
  UseGuards,
  CacheInterceptor,
  UseInterceptors,
  ClassSerializerInterceptor,
  ParseUUIDPipe,
  ConflictException,
  NotFoundException,
  Body,
  UnauthorizedException
} from '@nestjs/common'
import { ApiTags, ApiBearerAuth, ApiBody } from '@nestjs/swagger'
import { InjectRepository } from '@nestjs/typeorm'

import { UserEntity } from '@modules/user/entities/user.entity'
import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { Repository } from 'typeorm'

import { ServerMemberEntity } from '../entities/server-member.entity'
import { ServerEntity } from '../entities/server.entity'
import { AddMemberToServerUseCase } from '../use-cases/add-member-to-server/add-member-to-server.use-case'
import { FindServerByIdUseCase } from '../use-cases/find-server-by-id/find-server-by-id.use-case'

@Controller('/servers/:server_id/members')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
@ApiTags('members')
@ApiBearerAuth()
export class MemberController {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>,
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>,
    private readonly findServerById: FindServerByIdUseCase,
    private readonly addMemberToServer: AddMemberToServerUseCase
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard)
  public async index(@Param('server_id', new ParseUUIDPipe()) serverId: string) {
    await this.findServerById.execute(serverId)

    return this.userRepository
      .createQueryBuilder('u')
      .innerJoin(ServerMemberEntity, 'sm', 'sm.server_id = :serverId AND sm.member_id = u.id', { serverId })
      .getMany()
  }

  @Post('/')
  @UseGuards(JwtAuthGuard)
  @ApiBody({ schema: { example: { memberId: 'the user id to be added in the server' } } })
  public async store(
    @Param('server_id', new ParseUUIDPipe()) serverId: string,
    @Body('memberId', new ParseUUIDPipe()) memberId: string,
    @CurrentUserId() loggedUserId: string
  ) {
    if (memberId === loggedUserId) {
      throw new ConflictException('You cannot add yourself to a server')
    }

    const server = await this.findServerById.execute(serverId)

    if (server.ownerId !== loggedUserId) {
      throw new UnauthorizedException('You cannot add a user to this server as you are not the server owner')
    }

    await this.addMemberToServer.execute({
      server,
      userId: memberId
    })
  }

  @Delete('/')
  @UseGuards(JwtAuthGuard)
  @ApiBody({ schema: { example: { memberId: 'the user id to be removed from the server' } } })
  public async destroy(
    @Param('server_id', new ParseUUIDPipe()) serverId: string,
    @Body('memberId', new ParseUUIDPipe()) memberId: string,
    @CurrentUserId() loggedUserId: string
  ) {
    const server = await this.serverRepository.findOne(serverId)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    if (server.ownerId === memberId && memberId === loggedUserId) {
      throw new UnauthorizedException('You cannot remove yourself from the server as you are the server owner')
    }

    const userIsInServer = await this.memberRepository.findOne({
      memberId,
      serverId
    })

    if (!userIsInServer) {
      throw new NotFoundException('User is not in server')
    }

    if (loggedUserId !== memberId && server.ownerId !== loggedUserId) {
      throw new UnauthorizedException('You must be the server owner to remove another user from a server')
    }

    server.memberCount--
    await this.serverRepository.save(server)

    await this.memberRepository.delete({
      memberId,
      serverId
    })
  }
}
