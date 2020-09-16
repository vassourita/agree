import { Injectable, ExecutionContext, CanActivate, NotFoundException, UnauthorizedException } from '@nestjs/common'

import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { Request } from 'express'
import { getRepository } from 'typeorm'

@Injectable()
export class ServerMemberAuthGuard implements CanActivate {
  constructor(private readonly isMember: boolean = true) {}

  async canActivate(context: ExecutionContext): Promise<boolean> {
    const request = context.switchToHttp().getRequest<Request>()
    const serverId = request.params.server_id || request.params.serverId || request.body.serverId

    const server = await getRepository(ServerEntity).findOne(serverId)
    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    const memberId = request.user?.id
    if (!memberId) {
      throw new UnauthorizedException('You should be a member of the server to execute this action')
    }
    const member = await getRepository(ServerMemberEntity).findOne({
      where: {
        serverId,
        memberId
      }
    })

    return this.isMember ? !!member : !member
  }
}
