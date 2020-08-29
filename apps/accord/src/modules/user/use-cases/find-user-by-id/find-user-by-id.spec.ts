import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { FindUserByIdUseCase } from './find-user-by-id.use-case'

describe('FindUserByIdUseCase', () => {
  let sut: FindUserByIdUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity])
      ],
      providers: [FindUserByIdUseCase]
    }).compile()

    sut = moduleRef.get(FindUserByIdUseCase)
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

  it("should return a user found by it's id", async () => {
    const user = await getRepository(UserEntity).save({
      name: 'test user 1',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const result = await sut.execute({ id: user.id })
    const expected = expect.objectContaining({ name: user.name, tag: 1, email: 'test@user.com' })
    expect(result).toEqual(expected)
  })

  it('should throw if the user does not exists', async () => {
    await expect(sut.execute({ id: 'myrandommmuuid' })).rejects.toThrow('User not found')
  })
})
