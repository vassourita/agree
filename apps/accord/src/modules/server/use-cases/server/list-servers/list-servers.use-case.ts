import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { IListServersDTO } from './list-servers.dto'

@Injectable()
export class ListServersUseCase implements IUseCase<IListServersDTO, ServerEntity[]> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>
  ) {}

  execute(data: IListServersDTO): Promise<ServerEntity[]> {
    const page = data.page || 1
    const limit = data.limit || 15

    if (data.query) {
      return this.serverRepository
        .createQueryBuilder('s')
        .offset((page - 1) * limit)
        .limit(limit)
        .where("lower(replace(s.name, ' ', '')) LIKE lower(replace(:name, ' ', ''))", { name: `%${data.query}%` })
        .orderBy('s.member_count')
        .getMany()
    }

    return this.serverRepository
      .createQueryBuilder('s')
      .offset((page - 1) * limit)
      .limit(limit)
      .orderBy('s.member_count')
      .getMany()
  }
}
