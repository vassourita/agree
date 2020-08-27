import {
  Controller,
  Post,
  ClassSerializerInterceptor,
  CacheInterceptor,
  UseInterceptors,
  UseGuards,
  Param,
  ParseUUIDPipe,
  UnauthorizedException,
  NotFoundException,
  Body
} from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { Repository } from 'typeorm'

import { ServerMemberEntity } from '../entities/server-member.entity'
import { ServerEntity } from '../entities/server.entity'
import { ParseInviteTokenPipe } from '../pipes/parse-invite-token.pipe'
import { SignInviteTokenUseCase } from '../use-cases/sign-invite-token/sign-invite-token.use-case'

@Controller('/servers/invites')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
export class InviteController {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>,
    private readonly signInviteToken: SignInviteTokenUseCase
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

    const inviteToken = await this.signInviteToken.execute(serverId)

    return inviteToken
  }

  @Post('/:token/accept')
  @UseGuards(JwtAuthGuard)
  async accept(@Param('token', ParseInviteTokenPipe) serverId: string, @CurrentUserId() memberId: string) {
    const server = await this.serverRepository.findOne(serverId)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    const member = this.memberRepository.create({ memberId, serverId })

    await this.memberRepository.save(member)

    return server
  }
}
