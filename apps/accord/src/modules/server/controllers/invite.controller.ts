import {
  Controller,
  Post,
  ClassSerializerInterceptor,
  CacheInterceptor,
  UseInterceptors,
  UseGuards,
  ParseUUIDPipe,
  Body
} from '@nestjs/common'
import { ApiTags, ApiBearerAuth, ApiBody, ApiResponse } from '@nestjs/swagger'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { ParseExpireDatePipe } from '@shared/pipes/parse-expire-date.pipe'

import { ServerOwnerAuthGuard } from '../guards/server-owner/server-owner.guard'
import { AddMemberToServerUseCase } from '../use-cases/add-member-to-server/add-member-to-server.use-case'
import { DecodeInviteTokenUseCase } from '../use-cases/decode-invite-token/decode-invite-token.use-case'
import { FindServerByIdUseCase } from '../use-cases/find-server-by-id/find-server-by-id.use-case'
import { SignInviteTokenUseCase } from '../use-cases/sign-invite-token/sign-invite-token.use-case'
import { CreateInviteDTO } from './dtos/create-invite.dto'

@Controller('/servers/invites')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
@ApiTags('invites')
@ApiBearerAuth()
export class InviteController {
  constructor(
    private readonly signInviteToken: SignInviteTokenUseCase,
    private readonly decodeInviteToken: DecodeInviteTokenUseCase,
    private readonly addMemberToServer: AddMemberToServerUseCase,
    private readonly findServerById: FindServerByIdUseCase
  ) {}

  @Post('/')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  @ApiBody({ type: CreateInviteDTO })
  @ApiResponse({ schema: { example: { token: 'somebearerinvitetoken' } } })
  async generate(
    @Body('serverId', new ParseUUIDPipe()) serverId: string,
    @Body('expiresIn', new ParseExpireDatePipe()) expiresIn: string
  ) {
    await this.findServerById.execute(serverId)

    const token = await this.signInviteToken.execute({ serverId, expiresIn })

    return { token }
  }

  @Post('/accept')
  @UseGuards(JwtAuthGuard)
  @ApiBody({ schema: { example: { token: 'somebearerinvitetoken' } } })
  async accept(@Body('token') token: string, @CurrentUserId() userId: string) {
    const serverId = await this.decodeInviteToken.execute(token)

    const server = await this.findServerById.execute(serverId)

    await this.addMemberToServer.execute({
      server,
      userId
    })
  }
}
