import { Injectable, NotFoundException } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from 'src/shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { IFindUserByIdDTO } from './find-user-by-id.dto'

@Injectable()
export class FindUserByIdUseCase implements IUseCase<IFindUserByIdDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute({ id }: IFindUserByIdDTO) {
    const user = this.userRepository.findOne(id)

    if (!user) {
      throw new NotFoundException('User not found')
    }

    return user
  }
}
