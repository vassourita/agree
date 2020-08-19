import { Injectable, ConflictException } from '@nestjs/common'
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
    const userExists = await this.userRepository.findOne({
      where: { email: data.email }
    })

    if (userExists) {
      throw new ConflictException('Email already in use')
    }

    const user = this.userRepository.create(data)
    await this.userRepository.save(user)

    return user
  }
}
