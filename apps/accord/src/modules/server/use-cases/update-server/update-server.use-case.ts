import { Injectable } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { InjectRepository } from '@nestjs/typeorm'

import { ServerEntity } from '@modules/server/entities/server.entity'
import { IUseCase } from '@shared/protocols/use-case'
import { unlink } from 'fs'
import { join } from 'path'
import { Repository } from 'typeorm'

import { IUpdateServerDTO } from './update-server.dto'

@Injectable()
export class UpdateServerUseCase implements IUseCase<IUpdateServerDTO, ServerEntity> {
  constructor(
    @InjectRepository(ServerEntity)
    private readonly serverRepository: Repository<ServerEntity>,
    private readonly config: ConfigService
  ) {}

  async execute(data: IUpdateServerDTO): Promise<ServerEntity> {
    const { server, update } = data

    if (server.name !== update.name) {
      server.name = update.name
    }
    if (update.avatar) {
      const pathToOldAvatar = join(this.config.get('upload.dir'), server.avatar || '')
      unlink(pathToOldAvatar, () => 0)
      server.avatar = update.avatar
    }

    await this.serverRepository.save(server)

    return server
  }
}
