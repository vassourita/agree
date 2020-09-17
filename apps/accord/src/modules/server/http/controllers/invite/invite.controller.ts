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

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { ParseExpireDatePipe } from '@shared/pipes/parse-expire-date/parse-expire-date.pipe'

import { ServerOwnerAuthGuard } from '../../../guards/server-owner/server-owner.guard'
import { AddMemberToServerUseCase } from '../../../use-cases/add-member-to-server/add-member-to-server.use-case'
import { DecodeInviteTokenUseCase } from '../../../use-cases/decode-invite-token/decode-invite-token.use-case'
import { FindServerByIdUseCase } from '../../../use-cases/find-server-by-id/find-server-by-id.use-case'
import { SignInviteTokenUseCase } from '../../../use-cases/sign-invite-token/sign-invite-token.use-case'
import { InviteAcceptDocs } from './docs/invite-accept.docs'
import { InviteGenerateDocs } from './docs/invite-generate.docs'
import { InviteDocs } from './docs/invite.docs'

@Controller('/servers/invites')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
@InviteDocs()
export class InviteController {
  constructor(
    private readonly signInviteToken: SignInviteTokenUseCase,
    private readonly decodeInviteToken: DecodeInviteTokenUseCase,
    private readonly addMemberToServer: AddMemberToServerUseCase,
    private readonly findServerById: FindServerByIdUseCase
  ) {}

  @Post('/')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  @InviteGenerateDocs()
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
  @InviteAcceptDocs()
  async accept(@Body('token') token: string, @CurrentUserId() userId: string) {
    const serverId = await this.decodeInviteToken.execute(token)

    const server = await this.findServerById.execute(serverId)

    await this.addMemberToServer.execute({
      server,
      userId
    })
  }
}
