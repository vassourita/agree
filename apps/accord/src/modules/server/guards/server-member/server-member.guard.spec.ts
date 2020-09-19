import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { AddMemberToServerUseCase } from '@modules/server/use-cases/member/add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from '@modules/server/use-cases/server/create-server/create-server.use-case'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { getRepository } from 'typeorm'

import { ServerMemberAuthGuard } from './server-member.guard'

describe('ServerMemberAuthGuard', () => {
  let sut: ServerMemberAuthGuard
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
              expiresIn: config.get('auth.jwt.expiresIn'),
              issuer: config.get('auth.jwt.issuer')
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
    sut = new ServerMemberAuthGuard()
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
    await expect(() => sut.canActivate(mockExecutionContext as any)).rejects.toThrow('Authentication not found')

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: user.id }
    })
    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(true)
  })

  it('should return true/false based on if the user is member of the server or not and if true/false was passed in the constructor', async () => {
    sut = new ServerMemberAuthGuard(true)

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

    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(false)

    sut = new ServerMemberAuthGuard(false)

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: user.id }
    })

    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(false)

    mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
      params: { server_id: server.id },
      body: {},
      user: { id: 'someotherid' }
    })

    expect(await sut.canActivate(mockExecutionContext as any)).toEqual(true)
  })
})
