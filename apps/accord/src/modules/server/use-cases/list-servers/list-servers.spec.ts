import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { ListServersUseCase } from './list-servers.use-case'

describe('ListServersUseCase', () => {
  let sut: ListServersUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity])
      ],
      providers: [ListServersUseCase]
    }).compile()

    sut = moduleRef.get(ListServersUseCase)
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

    const user = await getRepository(UserEntity).save({
      name: 'test user 1',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const servers = [
      {
        name: 'my test server 1',
        ownerId: user.id
      },
      {
        name: 'my other test server 2',
        ownerId: user.id
      }
    ]

    await getRepository(ServerEntity).save(servers)

    const result = await sut.execute({})
    const expected = expect.arrayContaining(
      servers.map(s => expect.objectContaining({ name: s.name, ownerId: s.ownerId }))
    )

    expect(result).toEqual(expected)
  })

  it('should filter servers by name if one is passed', async () => {
    let name = 'my t'
    let result = await sut.execute({ query: name })

    expect(result).toEqual([expect.objectContaining({ name: 'my test server 1' })])

    name = 'other'
    result = await sut.execute({ query: name })

    expect(result).toEqual([expect.objectContaining({ name: 'my other test server 2' })])

    name = '2'
    result = await sut.execute({ query: name })

    expect(result).toEqual([expect.objectContaining({ name: 'my other test server 2' })])
  })

  it('should have pagination', async () => {
    const user = await getRepository(UserEntity).save({
      name: 'test user 2',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    for await (const i of [...new Array(15).keys()]) {
      await getRepository(ServerEntity).save({
        name: `my ${i} server`,
        ownerId: user.id
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
