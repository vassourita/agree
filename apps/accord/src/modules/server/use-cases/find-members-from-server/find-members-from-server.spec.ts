import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { AddMemberToServerUseCase } from '../add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '../create-server/create-server.use-case'
import { FindMembersFromServerUseCase } from './find-members-from-server.use-case'

describe('FindMembersFromServerUseCase', () => {
  let sut: FindMembersFromServerUseCase

  let addMember: AddMemberToServerUseCase
  let createServer: CreateServerUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [AddMemberToServerUseCase, CreateServerUseCase, FindMembersFromServerUseCase]
    }).compile()

    sut = moduleRef.get(FindMembersFromServerUseCase)
    createServer = moduleRef.get(CreateServerUseCase)
    addMember = moduleRef.get(AddMemberToServerUseCase)
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

  it('should return the users that are members from the server', async () => {
    const serverOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const server = await createServer.execute({
      name: 'test server',
      ownerId: serverOwner.id
    })

    for await (const i of [...Array(15).keys()]) {
      const newMember = await getRepository(UserEntity).save({
        name: `user ${i}`,
        email: `test@user${i}.com`,
        password: '123',
        tag: 1
      })

      await addMember.execute({
        server,
        userId: newMember.id
      })
    }

    const result = await sut.execute(server.id)

    expect(result.length).toEqual(16)
  })
})
