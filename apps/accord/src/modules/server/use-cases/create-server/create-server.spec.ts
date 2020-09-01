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
import { CreateServerUseCase } from './create-server.use-case'

describe('CreateServerUseCase', () => {
  let sut: CreateServerUseCase

  let sutServer: ServerEntity
  let sutServerOwner: UserEntity

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity])
      ],
      providers: [CreateServerUseCase, AddMemberToServerUseCase]
    }).compile()

    sut = moduleRef.get(CreateServerUseCase)
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

  it('should create and return a new server', async () => {
    sutServerOwner = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    sutServer = await sut.execute({
      name: 'my test server',
      ownerId: sutServerOwner.id
    })

    expect(sutServer).toBeDefined()
    expect(sutServer).toBeInstanceOf(ServerEntity)
  })

  it('should have added the owner as a server member', async () => {
    const result = await getRepository(ServerMemberEntity).findOne({
      serverId: sutServer.id,
      memberId: sutServerOwner.id
    })

    expect(result).toBeDefined()
    expect(result).toBeInstanceOf(ServerMemberEntity)
  })

  it('should have increased the member count to 1', async () => {
    expect(sutServer.memberCount).toBe(1)
  })
})
