import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { AddMemberToServerUseCase } from '@modules/server/use-cases/add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '@modules/server/use-cases/create-server/create-server.use-case'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { getRepository } from 'typeorm'

import { ServerOwnerAuthGuard } from './server-owner.guard'

describe('ServerOwnerAuthGuard', () => {
  let sut: ServerOwnerAuthGuard
  let createServer: CreateServerUseCase

  const mockExecutionContext = {
    switchToHttp: jest.fn().mockReturnThis(),
    getRequest: jest.fn().mockReturnThis()
  }

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [
        AccordConfigModule,
        DatabaseModule,
        TypeOrmModule.forFeature([ServerEntity, ServerMemberEntity, UserEntity, ChannelEntity]),
        JwtModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            secret: config.get('auth.key'),
            signOptions: {
              expiresIn: config.get('auth.jwt.expiresIn')
            }
          }),
          inject: [ConfigService]
        })
      ],
      providers: [JwtStrategy, CreateServerUseCase, AddMemberToServerUseCase]
    }).compile()

    createServer = moduleRef.get(CreateServerUseCase)
  })

  beforeEach(async () => {
    sut = new ServerOwnerAuthGuard()
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  it('should throw if no server is identified', async () => {
    const user = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: {},
      body: {},
      user: { id: 1 }
    })
    await expect(() => sut.canActivate(mockExecutionContext as any)).rejects.toThrow('Internal server error')

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: 'somenonexistingserverid' },
      body: {},
      user: { id: 1 }
    })
    await expect(() => sut.canActivate(mockExecutionContext as any)).rejects.toThrow('Server does not exists')

    const server = await createServer.execute({
      name: 'my test server',
      ownerId: user.id
    })

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: user.id }
    })
    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(true)
  })

  it('should throw if no user is identified on the request', async () => {
    const user = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const server = await createServer.execute({
      name: 'my test server',
      ownerId: user.id
    })

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: {}
    })
    await expect(() => sut.canActivate(mockExecutionContext as any)).rejects.toThrow(
      'You should be the server owner to execute this action'
    )

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: undefined }
    })
    await expect(() => sut.canActivate(mockExecutionContext as any)).rejects.toThrow(
      'You should be the server owner to execute this action'
    )

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: user.id }
    })
    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(true)
  })

  it('should return true/false based on if the user is or not the server owner', async () => {
    const user = await getRepository(UserEntity).save({
      name: 'server owner',
      email: 'test@user.com',
      password: '123',
      tag: 1
    })

    const server = await createServer.execute({
      name: 'my test server',
      ownerId: user.id
    })

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: user.id }
    })

    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(true)

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: 'someotherid' }
    })

    await expect(() => sut.canActivate(mockExecutionContext as any)).rejects.toThrow(
      'You should be the server owner to execute this action'
    )
  })
})
