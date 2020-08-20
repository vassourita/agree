import { Injectable, NotFoundException } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { IFindUserByNameAndTagDTO } from './find-user-by-name-and-tag.dto'

@Injectable()
export class FindUserByNameAndTagUseCase implements IUseCase<IFindUserByNameAndTagDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}

  async execute(query: IFindUserByNameAndTagDTO) {
    const user = await this.userRepository.findOne({
      where: query
    })

    if (!user) {
      throw new NotFoundException('User not found')
    }

    return user
  }
}
