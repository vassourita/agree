import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { FindServersByOwnerUseCase } from './find-servers-by-owner.use-case'

describe('FindServersByOwnerUseCase', () => {
  let sut: FindServersByOwnerUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [FindServersByOwnerUseCase]
    }).compile()

    sut = moduleRef.get(FindServersByOwnerUseCase)
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

  it('should return a list of servers', async () => {
    const user = await getRepository(UserEntity).save({
      name: 'test user 1',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    let result = (await sut.execute(user.id)).length
    expect(result).toEqual(0)

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

    result = (await sut.execute(user.id)).length
    expect(result).toEqual(2)
  })
})
