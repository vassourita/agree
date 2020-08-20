import { Injectable } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'

@Injectable()
export class ListUsersUseCase implements IUseCase<any, UserEntity[]> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute() {
    return this.userRepository.find()
  }
}
