import {
  Controller,
  Post,
  ClassSerializerInterceptor,
  CacheInterceptor,
  UseInterceptors,
  UseGuards,
  ParseUUIDPipe,
  UnauthorizedException,
  NotFoundException,
  Body,
  ConflictException
} from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { Repository } from 'typeorm'

import { ServerMemberEntity } from '../entities/server-member.entity'
import { ServerEntity } from '../entities/server.entity'
import { DecodeInviteTokenUseCase } from '../use-cases/decode-invite-token/decode-invite-token.use-case'
import { SignInviteTokenUseCase } from '../use-cases/sign-invite-token/sign-invite-token.use-case'

@Controller('/servers/invites')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
export class InviteController {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>,
    private readonly signInviteToken: SignInviteTokenUseCase,
    private readonly decodeInviteToken: DecodeInviteTokenUseCase
  ) {}

  @Post('/')
  @UseGuards(JwtAuthGuard)
  async generate(@Body('serverId', new ParseUUIDPipe()) serverId: string, @CurrentUserId() userId: string) {
    const server = await this.serverRepository.findOne(serverId)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    if (server.ownerId !== userId) {
      throw new UnauthorizedException('You cannot create a invite token as you are not the server owner')
    }

    const token = await this.signInviteToken.execute(serverId)

    return { token }
  }

  @Post('/accept')
  @UseGuards(JwtAuthGuard)
  async accept(@Body('token') token: string, @CurrentUserId() memberId: string) {
    const serverId = await this.decodeInviteToken.execute(token)

    const server = await this.serverRepository.findOne(serverId)

    if (!server) {
      throw new NotFoundException('Server does not exists anymore')
    }

    const userIsInServer = await this.memberRepository.findOne({
      memberId,
      serverId
    })

    if (userIsInServer) {
      throw new ConflictException('User is already in server')
    }

    const member = this.memberRepository.create({ memberId, serverId })
    server.memberCount++

    await this.memberRepository.save(member)
    await this.serverRepository.save(server)

    return member
  }
}
