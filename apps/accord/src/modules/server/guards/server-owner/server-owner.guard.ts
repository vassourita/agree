import {
  Injectable,
  ExecutionContext,
  CanActivate,
  NotFoundException,
  UnauthorizedException,
  InternalServerErrorException
} from '@nestjs/common'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { Request } from 'express'
import { getRepository } from 'typeorm'

@Injectable()
export class ServerOwnerAuthGuard implements CanActivate {
  async canActivate(context: ExecutionContext): Promise<boolean> {
    const request = context.switchToHttp().getRequest<Request>()
    const serverId = request.params.server_id || request.params.serverId || request.body.serverId
    if (!serverId) {
      throw new InternalServerErrorException()
    }

    const server = await getRepository(ServerEntity).findOne(serverId)
    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    const userId = request.user?.id
    if (userId !== server.ownerId) {
      throw new UnauthorizedException()
    }

    return true
  }
}
