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
  Body
} from '@nestjs/common'

import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'

import { ServerMemberAuthGuard } from '../../../guards/server-member/server-member.guard'
import { ServerOwnerAuthGuard } from '../../../guards/server-owner/server-owner.guard'
import { AddMemberToServerUseCase } from '../../../use-cases/add-member-to-server/add-member-to-server.use-case'
import { FindMembersFromServerUseCase } from '../../../use-cases/find-members-from-server/find-members-from-server.use-case'
import { FindServerByIdUseCase } from '../../../use-cases/find-server-by-id/find-server-by-id.use-case'
import { RemoveMemberUseCase } from '../../../use-cases/remove-member/remove-member.use-case'
import { MemberDestroyDocs } from './docs/member-destroy.docs'
import { MemberStoreDocs } from './docs/member-store.docs'
import { MemberDocs } from './docs/member.docs'

@Controller('/servers/:server_id/members')
@UseInterceptors(CacheInterceptor, ClassSerializerInterceptor)
@MemberDocs()
export class MemberController {
  constructor(
    private readonly findServerById: FindServerByIdUseCase,
    private readonly addMemberToServer: AddMemberToServerUseCase,
    private readonly findMembersFromServer: FindMembersFromServerUseCase,
    private readonly removeMember: RemoveMemberUseCase
  ) {}

  @Get('/')
  @UseGuards(JwtAuthGuard, new ServerMemberAuthGuard())
  public async index(@Param('server_id', new ParseUUIDPipe()) serverId: string) {
    await this.findServerById.execute(serverId)

    return this.findMembersFromServer.execute(serverId)
  }

  @Post('/')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  @MemberStoreDocs()
  public async store(
    @Param('server_id', new ParseUUIDPipe()) serverId: string,
    @Body('memberId', new ParseUUIDPipe()) memberId: string,
    @CurrentUserId() loggedUserId: string
  ) {
    if (memberId === loggedUserId) {
      throw new ConflictException('You cannot add yourself to a server')
    }

    const server = await this.findServerById.execute(serverId)

    await this.addMemberToServer.execute({
      server,
      userId: memberId
    })
  }

  @Delete('/')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  @MemberDestroyDocs()
  public async destroy(
    @Param('server_id', new ParseUUIDPipe()) serverId: string,
    @Body('memberId', new ParseUUIDPipe()) memberId: string,
    @CurrentUserId() loggedUserId: string
  ) {
    const server = await this.findServerById.execute(serverId)

    await this.removeMember.execute({
      loggedUserId,
      memberId,
      server
    })
  }
}
