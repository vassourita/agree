import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { FindUserByEmailUseCase } from './find-user-by-email.use-case'

describe('FindUserByEmailUseCase', () => {
  let sut: FindUserByEmailUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity])
      ],
      providers: [FindUserByEmailUseCase]
    }).compile()

    sut = moduleRef.get(FindUserByEmailUseCase)
  })

  afterAll(async () => {
    await getRepository(UserEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerEntity).createQueryBuilder().delete().execute()
    await getRepository(ServerMemberEntity).createQueryBuilder().delete().execute()
  })

  it('should be defined', async () => {
    expect(sut).toBeDefined()
    expect(sut.execute).toBeDefined()
  })

  it("should return a user found by it's email", async () => {
    const user = await getRepository(UserEntity).save({
      name: 'test user 1',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const result = await sut.execute({ email: user.email })
    const expected = expect.objectContaining({ name: user.name, tag: 1 })
    expect(result).toEqual(expected)
  })

  it('should throw if the user does not exists', async () => {
    await expect(sut.execute({ email: 'some_random@email.com' })).rejects.toThrow('User not found')
  })
})
