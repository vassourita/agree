import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { IDeleteServerDTO } from './delete-server.dto'

@Injectable()
export class DeleteServerUseCase implements IUseCase<IDeleteServerDTO, void> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>
  ) {}

  async execute(data: IDeleteServerDTO): Promise<void> {
    await this.serverRepository.delete({
      id: data.server.id
    })
  }
}
