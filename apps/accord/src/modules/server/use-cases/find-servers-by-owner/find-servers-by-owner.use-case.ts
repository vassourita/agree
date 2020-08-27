import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

@Injectable()
export class FindServersByOwnerUseCase implements IUseCase<string, ServerEntity[]> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>
  ) {}

  async execute(id: string): Promise<ServerEntity[]> {
    return this.serverRepository.find({
      where: { ownerId: id }
    })
  }
}
