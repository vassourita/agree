import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { FindServerByIdUseCase } from './find-server-by-id.use-case'

describe('FindServerByIdUseCase', () => {
  let sut: FindServerByIdUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [FindServerByIdUseCase]
    }).compile()

    sut = moduleRef.get(FindServerByIdUseCase)
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

  it("should return a server found by it's id", async () => {
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

    const [server1, server2] = await getRepository(ServerEntity).save(servers)

    let result = await sut.execute(server1.id)
    let expected = expect.objectContaining({ name: server1.name, ownerId: server1.ownerId })
    expect(result).toEqual(expected)

    result = await sut.execute(server2.id)
    expected = expect.objectContaining({ name: server2.name, ownerId: server2.ownerId })
    expect(result).toEqual(expected)
  })

  it('should throw if the server does not exists', async () => {
    await expect(sut.execute('somerandomid')).rejects.toThrow('Server does not exists')
  })
})
