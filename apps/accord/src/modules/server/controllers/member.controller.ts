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
import { InjectRepository } from '@nestjs/typeorm'

import { UserEntity } from '@modules/user/entities/user.entity'
import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { Repository } from 'typeorm'

import { ServerMemberEntity } from '../entities/server-member.entity'
import { ServerEntity } from '../entities/server.entity'

@Controller('/servers/:server_id/members')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
export class MemberController {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>,
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard)
  public async index(@Param('server_id', new ParseUUIDPipe()) serverId: string) {
    const serverExists = await this.serverRepository.findOne(serverId)

    if (!serverExists) {
      throw new NotFoundException('Server does not exists')
    }

    return this.userRepository
      .createQueryBuilder('u')
      .innerJoin(ServerMemberEntity, 'sm', 'sm.server_id = :serverId AND sm.member_id = u.id', { serverId })
      .getMany()
  }

  @Post('/')
  @UseGuards(JwtAuthGuard)
  public async store(@Param('server_id', new ParseUUIDPipe()) serverId: string, @CurrentUserId() memberId: string) {
    const server = await this.serverRepository.findOne(serverId)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    const userIsInServer = await this.memberRepository.findOne({
      memberId,
      serverId
    })

    if (userIsInServer) {
      throw new ConflictException('User is already in server')
    }

    const member = this.memberRepository.create({
      memberId,
      serverId
    })
    server.memberCount++

    await this.memberRepository.save(member)
    await this.serverRepository.save(server)

    return member
  }

  @Delete('/')
  @UseGuards(JwtAuthGuard)
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
