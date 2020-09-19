import { UnauthorizedException, NotFoundException } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { IRemoveMemberDTO } from './remove-member.dto'

export class RemoveMemberUseCase implements IUseCase<IRemoveMemberDTO, void> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>
  ) {}

  async execute(data: IRemoveMemberDTO): Promise<void> {
    const { server, memberId, loggedUserId } = data

    if (server.ownerId === memberId && memberId === loggedUserId) {
      throw new UnauthorizedException('You cannot remove yourself from the server as you are the server owner')
    }

    const userIsInServer = await this.memberRepository.findOne({
      memberId,
      serverId: server.id
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
      serverId: server.id
    })
  }
}
