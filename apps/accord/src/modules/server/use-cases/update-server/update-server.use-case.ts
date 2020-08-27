import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { IUpdateServerDTO } from './update-server.dto'

@Injectable()
export class UpdateServerUseCase implements IUseCase<IUpdateServerDTO, ServerEntity> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>
  ) {}

  async execute(data: IUpdateServerDTO): Promise<ServerEntity> {
    const { server, update } = data

    if (server.name !== update.name) {
      server.name = update.name
      await this.serverRepository.save(server)
    }

    return server
  }
}
