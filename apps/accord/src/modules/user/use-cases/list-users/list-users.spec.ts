import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { ListUsersUseCase } from './list-users.use-case'

describe('ListUsersUseCase', () => {
  let sut: ListUsersUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity])
      ],
      providers: [ListUsersUseCase]
    }).compile()

    sut = moduleRef.get(ListUsersUseCase)
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

  it('should return a list of servers', async () => {
    expect(await sut.execute({})).toEqual([])

    const user1 = await getRepository(UserEntity).save({
      name: 'test user 1',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const user2 = await getRepository(UserEntity).save({
      name: 'test user 2',
      email: 'test@user2.com',
      password: '123',
      tag: 1
    })

    await getRepository(UserEntity).save([user1, user2])

    const result = await sut.execute({})
    const expected = expect.arrayContaining(
      [user1, user2].map(u => expect.objectContaining({ name: u.name, tag: u.tag }))
    )

    expect(result).toEqual(expected)
  })

  it('should have pagination', async () => {
    for await (const i of [...new Array(15).keys()]) {
      await getRepository(UserEntity).save({
        name: `test user ${i}`,
        email: `test@user${i}.com`,
        password: '123',
        tag: i
      })
    }

    let result = await sut.execute({})
    expect(result.length).toEqual(15) // base page limit

    result = await sut.execute({ page: 1 })
    expect(result.length).toEqual(15)

    result = await sut.execute({ page: 2 })
    expect(result.length).toEqual(2)

    result = await sut.execute({ page: 3 })
    expect(result.length).toEqual(0)

    result = await sut.execute({ limit: 20 })
    expect(result.length).toEqual(2 + 15)

    result = await sut.execute({ page: 1, limit: 10 })
    expect(result.length).toEqual(10)

    result = await sut.execute({ page: 2, limit: 10 })
    expect(result.length).toEqual(7)
  })
})
