import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { CreateServerUseCase } from '../create-server/create-server.use-case'
import { AddMemberToServerUseCase } from './add-member-to-server.use-case'

describe('AddMemberToServerUseCase', () => {
  let sut: AddMemberToServerUseCase

  let sutServer: ServerEntity
  let sutServerOwner: UserEntity

  let createServer: CreateServerUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [AddMemberToServerUseCase, CreateServerUseCase]
    }).compile()

    sut = moduleRef.get(AddMemberToServerUseCase)
    createServer = moduleRef.get(CreateServerUseCase)
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

  it("should increase the server's member count", async () => {
    sutServerOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    sutServer = await createServer.execute({
      name: 'my test server',
      ownerId: sutServerOwner.id
    })

    expect(sutServer.memberCount).toEqual(1)

    const newMember = await getRepository(UserEntity).save({
      name: 'new member of server',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    await sut.execute({ server: sutServer, userId: newMember.id })

    expect(sutServer.memberCount).toEqual(2)
  })

  it('should have created a new Server Member instance', async () => {
    const result = await getRepository(ServerMemberEntity).find({
      where: { serverId: sutServer.id }
    })

    expect(result.length).toBe(2)
  })

  it('should throw if the user is already in the server', async () => {
    await expect(
      sut.execute({
        server: sutServer,
        userId: sutServerOwner.id
      })
    ).rejects.toThrow('User is already in server')
  })
})
