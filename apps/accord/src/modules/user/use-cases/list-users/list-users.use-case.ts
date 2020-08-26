import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { IListUsersDTO } from './list-users.dto'

@Injectable()
export class ListUsersUseCase implements IUseCase<IListUsersDTO, UserEntity[]> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute(pagination: IListUsersDTO) {
    const page = pagination.page || 1
    const limit = pagination.limit || 15

    return this.userRepository.find({
      skip: page * limit,
      take: limit
    })
  }
}
