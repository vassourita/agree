import { InjectRepository } from '@nestjs/typeorm'

import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

export class FindMembersFromServerUseCase implements IUseCase<string, UserEntity[]> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute(serverId: string): Promise<UserEntity[]> {
    return this.userRepository
      .createQueryBuilder('u')
      .innerJoin(ServerMemberEntity, 'sm', 'sm.server_id = :serverId AND sm.member_id = u.id', { serverId })
      .getMany()
  }
}
