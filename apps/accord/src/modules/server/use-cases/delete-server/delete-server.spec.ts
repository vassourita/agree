import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { getRepository } from 'typeorm'

import { AddMemberToServerUseCase } from '../add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '../create-server/create-server.use-case'
import { DeleteServerUseCase } from './delete-server.use-case'

describe('DeleteServerUseCase', () => {
  let sut: DeleteServerUseCase

  let createServer: CreateServerUseCase

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([UserEntity, ServerEntity, ServerMemberEntity])
      ],
      providers: [DeleteServerUseCase, CreateServerUseCase, AddMemberToServerUseCase]
    }).compile()

    sut = moduleRef.get(DeleteServerUseCase)
    createServer = moduleRef.get(CreateServerUseCase)
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

  it('should delete a server', async () => {
    const sutServerOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const sutServer = await createServer.execute({
      name: 'my test server',
      ownerId: sutServerOwner.id
    })

    expect((await getRepository(ServerEntity).find()).length).toEqual(1)

    await sut.execute({
      server: sutServer,
      userId: sutServerOwner.id
    })

    expect((await getRepository(ServerEntity).find()).length).toEqual(0)
  })

  it('should throw if the user is not the server owner', async () => {
    const sutServerOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const sutServer = await createServer.execute({
      name: 'my test server',
      ownerId: sutServerOwner.id
    })

    await expect(
      sut.execute({
        server: sutServer,
        userId: 'someotherid123'
      })
    ).rejects.toThrow('You might be the server owner to delete the server')
  })
})
