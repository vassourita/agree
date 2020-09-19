import { ConflictException, Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { getConnection, Repository } from 'typeorm'

import { IAddMemberToServerDTO } from './add-member-to-server.dto'

@Injectable()
export class AddMemberToServerUseCase implements IUseCase<IAddMemberToServerDTO, void> {
  constructor(
    @InjectRepository(ServerMemberEntity)
    private readonly memberRepository: Repository<ServerMemberEntity>
  ) {}

  async execute(data: IAddMemberToServerDTO): Promise<void> {
    const userIsInServer = await this.memberRepository.findOne({
      memberId: data.userId,
      serverId: data.server.id
    })

    if (userIsInServer) {
      throw new ConflictException('User is already in server')
    }

    await getConnection().transaction(async entityManager => {
      const serverOwnerAsMember = entityManager.create(ServerMemberEntity, {
        memberId: data.userId,
        serverId: data.server.id
      })
      await entityManager.save(ServerMemberEntity, serverOwnerAsMember)

      data.server.memberCount++
      await entityManager.save(ServerEntity, data.server)
    })
  }
}
