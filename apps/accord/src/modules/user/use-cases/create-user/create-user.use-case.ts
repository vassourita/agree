import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from 'src/shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { ICreateUserDTO } from './create-user.dto'

@Injectable()
export class CreateUserUseCase implements IUseCase<ICreateUserDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute(data: ICreateUserDTO) {
    const user = this.userRepository.create(data)
    await this.userRepository.save(user)

    return user
  }
}
