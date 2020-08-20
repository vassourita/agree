import { Injectable, NotFoundException } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from 'src/shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { FindUserByIdDTO } from './find-user-by-id.dto'

@Injectable()
export class FindUserByIdUseCase implements IUseCase<FindUserByIdDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute({ id }: FindUserByIdDTO) {
    const user = this.userRepository.findOne(id)

    if (!user) {
      throw new NotFoundException('User not found')
    }

    return user
  }
}
