import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { AuthProvider } from '@shared/providers/auth.provider'
import { getRepository } from 'typeorm'

import { CreateUserUseCase } from './create-user.use-case'

describe('CreateUserUseCase', () => {
  let sut: CreateUserUseCase

  let sutUser: UserEntity

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity]),
        JwtModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            secret: config.get('auth.key'),
            signOptions: {
              expiresIn: config.get('auth.jwt.expiresIn'),
              issuer: config.get('auth.jwt.issuer')
            }
          }),
          inject: [ConfigService]
        })
      ],
      providers: [CreateUserUseCase, AuthProvider]
    }).compile()

    sut = moduleRef.get(CreateUserUseCase)
  })

  afterAll(async () => {
    await getRepository(UserEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerMemberEntity).createQueryBuilder().delete().execute()
    await getRepository(ChannelEntity).createQueryBuilder().delete().execute()
  })

  it('should be defined', async () => {
    expect(sut).toBeDefined()
    expect(sut.execute).toBeDefined()
  })

  it('should create and return a new user', async () => {
    sutUser = await sut.execute({
      name: 'test user',
      email: 'test@user.com',
      password: '1234567'
    })

    expect(sutUser).toBeDefined()
    expect(sutUser).toBeInstanceOf(UserEntity)
  })

  it('should hash the user password', async () => {
    const passwordIsHashed = sutUser.password !== '1234567'

    expect(passwordIsHashed).toBe(true)
  })

  it('should create a tag for the user', async () => {
    expect(sutUser.tag).toBeDefined()
  })

  it('should create a new tag if the name is already in use', async () => {
    const newUser = await sut.execute({
      name: 'test user',
      email: 'test@user2.com',
      password: '1234567'
    })

    const tagsAreDifferent = newUser.tag !== sutUser.tag

    expect(tagsAreDifferent).toBe(true)
  })

  it('should throw if the email is already in use', async () => {
    await expect(
      sut.execute({
        name: 'test user 3',
        email: 'test@user.com',
        password: '1234567'
      })
    ).rejects.toThrow('Email already in use')
  })
})
