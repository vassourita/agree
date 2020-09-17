import { Injectable } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { unlink } from 'fs'
import { join } from 'path'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { IUpdateUserDTO } from './update-user.dto'

@Injectable()
export class UpdateUserUseCase implements IUseCase<IUpdateUserDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>,
    private readonly config: ConfigService
  ) {}

  async execute(data: IUpdateUserDTO) {
    const user = await this.userRepository.findOne(data.id)

    if (data.name !== user.name) {
      user.name = data.name
    }
    if (data.status !== user.status) {
      user.status = data.status
    }
    if (data.avatar) {
      const pathToOldAvatar = join(this.config.get('upload.dir'), user.avatar || '')
      unlink(pathToOldAvatar, () => 0)
      user.avatar = data.avatar
    }

    await this.userRepository.save(user)

    return user
  }
}
