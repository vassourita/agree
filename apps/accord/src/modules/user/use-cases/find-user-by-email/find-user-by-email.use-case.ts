import { Injectable, NotFoundException } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { FindUserByEmailDTO } from './find-user-by-email.dto'

@Injectable()
export class FindUserByEmailUseCase implements IUseCase<FindUserByEmailDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute({ email }: FindUserByEmailDTO) {
    const user = await this.userRepository.findOne({
      where: { email }
    })

    if (!user) {
      throw new NotFoundException('User not found')
    }

    return user
  }
}
