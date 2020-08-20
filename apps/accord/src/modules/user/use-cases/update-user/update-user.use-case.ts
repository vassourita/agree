import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { UpdateUserDTO } from './update-user.dto'

@Injectable()
export class UpdateUserUseCase implements IUseCase<UpdateUserDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute(data: UpdateUserDTO) {
    const user = await this.userRepository.findOne(data.id)

    if (data.name !== user.name) {
      user.name = data.name
    }
    if (data.status !== user.status) {
      user.status = data.status
    }

    await this.userRepository.save(user)

    return user
  }
}
