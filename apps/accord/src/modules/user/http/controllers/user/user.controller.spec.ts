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
import { UserEntity } from '@modules/user/entities/user.entity'
import { CreateUserUseCase } from '@modules/user/use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '@modules/user/use-cases/find-user-by-id/find-user-by-id.use-case'
import { FindUserByNameAndTagUseCase } from '@modules/user/use-cases/find-user-by-name-and-tag/find-user-by-name-and-tag.use-case'
import { ListUsersUseCase } from '@modules/user/use-cases/list-users/list-users.use-case'
import { UpdateUserUseCase } from '@modules/user/use-cases/update-user/update-user.use-case'
import { DatabaseModule } from '@shared/database/database.module'
import { RedisClientNames } from '@shared/database/redis.service'
import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'
import redisStore from 'cache-manager-ioredis'

import { UserController } from './user.controller'

describe('UserController', () => {
  let sut: UserController

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
        CreateUserUseCase,
        FindUserByIdUseCase,
        FindUserByNameAndTagUseCase,
        ListUsersUseCase,
        UpdateUserUseCase,
        AuthProvider,
        JwtStrategy
      ],
      controllers: [UserController]
    }).compile()

    sut = moduleRef.get(UserController)
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  describe('GET /users', () => {
    it('should be defined', () => {
      expect(sut.index).toBeDefined()
    })
  })

  describe('GET /users/@me', () => {
    it('should be defined', () => {
      expect(sut.me).toBeDefined()
    })
  })

  describe('GET /users/:nametag', () => {
    it('should be defined', () => {
      expect(sut.show).toBeDefined()
    })
  })

  describe('POST /users', () => {
    it('should be defined', () => {
      expect(sut.store).toBeDefined()
    })
  })

  describe('PUT /users', () => {
    it('should be defined', () => {
      expect(sut.update).toBeDefined()
    })
  })
})
