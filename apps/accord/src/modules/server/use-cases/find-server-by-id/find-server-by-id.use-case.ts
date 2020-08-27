import { NotFoundException, Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

@Injectable()
export class FindServerByIdUseCase implements IUseCase<string, ServerEntity> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>
  ) {}

  async execute(id: string): Promise<ServerEntity> {
    const server = await this.serverRepository.findOne(id)

    if (!server) {
      throw new NotFoundException('Server does not exists')
    }

    return server
  }
}
