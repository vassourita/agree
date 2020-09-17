import { CacheModule } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { MulterModule } from '@nestjs/platform-express'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { AddMemberToServerUseCase } from '@modules/server/use-cases/add-member-to-server/add-member-to-server.use-case'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { RedisClientNames } from '@shared/database/redis.service'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import redisStore from 'cache-manager-ioredis'

import { CreateServerUseCase } from '../../../use-cases/create-server/create-server.use-case'
import { DeleteServerUseCase } from '../../../use-cases/delete-server/delete-server.use-case'
import { FindServerByIdUseCase } from '../../../use-cases/find-server-by-id/find-server-by-id.use-case'
import { FindServersByOwnerUseCase } from '../../../use-cases/find-servers-by-owner/find-servers-by-owner.use-case'
import { ListServersUseCase } from '../../../use-cases/list-servers/list-servers.use-case'
import { UpdateServerUseCase } from '../../../use-cases/update-server/update-server.use-case'
import { ServerController } from './server.controller'

describe('ServerController', () => {
  let sut: ServerController

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
        }),
        CacheModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            store: redisStore,
            host: config.get('database.redis.host'),
            port: config.get('database.redis.port'),
            password: config.get('database.redis.password'),
            ttl: config.get('database.redis.ttl'),
            keyPrefix: RedisClientNames.CACHE
          }),
          inject: [ConfigService]
        }),
        MulterModule.registerAsync({
          useFactory: (config: ConfigService) => ({
            dest: config.get('upload.dir'),
            storage: config.get('upload.storage'),
            fileFilter: config.get('upload.filter')
          }),
          inject: [ConfigService]
        })
      ],
      providers: [
        ListServersUseCase,
        FindServerByIdUseCase,
        FindServersByOwnerUseCase,
        CreateServerUseCase,
        UpdateServerUseCase,
        DeleteServerUseCase,
        AddMemberToServerUseCase,
        JwtStrategy
      ],
      controllers: [ServerController]
    }).compile()

    sut = moduleRef.get(ServerController)
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  describe('GET /servers', () => {
    it('should be defined', () => {
      expect(sut.index).toBeDefined()
    })
  })

  describe('GET /servers/@me', () => {
    it('should be defined', () => {
      expect(sut.me).toBeDefined()
    })
  })

  describe('GET /servers/:server_id', () => {
    it('should be defined', () => {
      expect(sut.show).toBeDefined()
    })
  })

  describe('POST /servers', () => {
    it('should be defined', () => {
      expect(sut.store).toBeDefined()
    })
  })

  describe('PUT /servers/:server_id', () => {
    it('should be defined', () => {
      expect(sut.update).toBeDefined()
    })
  })

  describe('DELETE /servers/:server_id', () => {
    it('should be defined', () => {
      expect(sut.destroy).toBeDefined()
    })
  })
})
