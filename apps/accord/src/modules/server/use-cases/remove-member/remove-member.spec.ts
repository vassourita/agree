import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { AddMemberToServerUseCase } from '../add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '../create-server/create-server.use-case'
import { FindMembersFromServerUseCase } from '../find-members-from-server/find-members-from-server.use-case'
import { RemoveMemberUseCase } from './remove-member.use-case'

describe('RemoveMemberUseCase', () => {
  let sut: RemoveMemberUseCase
  let serverOwner: UserEntity
  let server: ServerEntity

  let createServer: CreateServerUseCase
  let addMember: AddMemberToServerUseCase
  let findMembers: FindMembersFromServerUseCase

  const users: UserEntity[] = []

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [AddMemberToServerUseCase, CreateServerUseCase, RemoveMemberUseCase, FindMembersFromServerUseCase]
    }).compile()

    sut = moduleRef.get(RemoveMemberUseCase)
    createServer = moduleRef.get(CreateServerUseCase)
    addMember = moduleRef.get(AddMemberToServerUseCase)
    findMembers = moduleRef.get(FindMembersFromServerUseCase)
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

  it("should remove users from server by deleting server-member instances and reduce the servers's member count", async () => {
    serverOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    server = await createServer.execute({
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
      users.push(newMember)

      await addMember.execute({
        server,
        userId: newMember.id
      })
    }

    let result = await findMembers.execute(server.id)

    expect(result.length).toEqual(16)
    expect(server.memberCount).toEqual(16)

    await sut.execute({
      loggedUserId: serverOwner.id,
      memberId: users[0].id,
      server
    })

    result = await findMembers.execute(server.id)

    expect(result.length).toEqual(15)
    expect(server.memberCount).toEqual(15)
  })

  it('should not remove the member if it is the server owner', async () => {
    await expect(
      sut.execute({
        loggedUserId: serverOwner.id,
        memberId: serverOwner.id,
        server
      })
    ).rejects.toThrow('You cannot remove yourself from the server as you are the server owner')
  })

  it('should throw if the user to be deleted is not a member of the server', async () => {
    await expect(
      sut.execute({
        loggedUserId: serverOwner.id,
        memberId: 'someuuid',
        server
      })
    ).rejects.toThrow('User is not in server')
  })

  it('should throw if a user tries to remove another from the server and it is not the owner', async () => {
    await expect(
      sut.execute({
        loggedUserId: users[2].id,
        memberId: users[1].id,
        server
      })
    ).rejects.toThrow('You must be the server owner to remove another user from a server')
  })
})
