import { ConflictException, Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { IAddMemberToServerDTO } from './add-member-to-server.dto'

@Injectable()
export class AddMemberToServerUseCase implements IUseCase<IAddMemberToServerDTO, void> {
  constructor(
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>,
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>
  ) {}

  async execute(data: IAddMemberToServerDTO): Promise<void> {
    const userIsInServer = await this.memberRepository.findOne({
      memberId: data.userId,
      serverId: data.server.id
    })

    if (userIsInServer) {
      throw new ConflictException('User is already in server')
    }

    const serverOwnerAsMember = this.memberRepository.create({
      memberId: data.userId,
      serverId: data.server.id
    })
    await this.memberRepository.save(serverOwnerAsMember)

    data.server.memberCount++
    await this.serverRepository.save(data.server)
  }
}
