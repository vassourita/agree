import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { FindUserByNameAndTagUseCase } from './find-user-by-name-and-tag.use-case'

describe('FindUserByNameAndTagUseCase', () => {
  let sut: FindUserByNameAndTagUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [FindUserByNameAndTagUseCase]
    }).compile()

    sut = moduleRef.get(FindUserByNameAndTagUseCase)
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

  it("should return a user found by it's name and tag", async () => {
    const user1 = await getRepository(UserEntity).save({
      name: 'test user 1',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })
    let result = await sut.execute({ name: user1.name, tag: user1.tag })
    let expected = expect.objectContaining({ name: user1.name, tag: user1.tag, email: user1.email })
    expect(result).toEqual(expected)

    const user2 = await getRepository(UserEntity).save({
      name: 'testuser2',
      email: 'test@user.com',
      password: '123',
      tag: 25
    })

    result = await sut.execute({ name: user2.name, tag: user2.tag })
    expected = expect.objectContaining({ name: user2.name, tag: user2.tag, email: user2.email })
    expect(result).toEqual(expected)
  })

  it('should throw if the user does not exists', async () => {
    await expect(sut.execute({ name: 'somerandomname', tag: 99 })).rejects.toThrow('User not found')
  })
})
