import { Injectable, ConflictException } from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import { AuthProvider } from '@shared/providers/auth.provider'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { CreateUserDTO } from './create-user.dto'

@Injectable()
export class CreateUserUseCase implements IUseCase<CreateUserDTO, UserEntity> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>,
    private readonly auth: AuthProvider
  ) {}

  async execute(data: CreateUserDTO) {
    const emailInUse = await this.userRepository.findOne({
      where: { email: data.email }
    })

    if (emailInUse) {
      throw new ConflictException('Email already in use')
    }

    const nameInUse = await this.userRepository.findOne({
      where: { name: data.name },
      order: { tag: 'DESC' }
    })

    const hashed = await this.auth.hashPassword(data.password)

    const user = this.userRepository.create({
      ...data,
      tag: nameInUse?.tag ? nameInUse?.tag + 1 : 1,
      password: hashed
    })
    await this.userRepository.save(user)

    return user
  }
}
