import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { AddMemberToServerUseCase } from '../../member/add-member-to-server/add-member-to-server.use-case'
import { ICreateServerDTO } from './create-server.dto'

@Injectable()
export class CreateServerUseCase implements IUseCase<ICreateServerDTO, ServerEntity> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    private readonly addMemberToServer: AddMemberToServerUseCase
  ) {}

  async execute(data: ICreateServerDTO): Promise<ServerEntity> {
    const server = this.serverRepository.create({
      name: data.name,
      ownerId: data.ownerId
    })

    await this.serverRepository.save(server)

    await this.addMemberToServer.execute({
      server,
      userId: server.ownerId
    })

    return server
  }
}
