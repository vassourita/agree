import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import * as uuid from 'uuid'

import { JwtType } from './dtos/jwt-payload.dto'
import { JwtStrategy } from './jwt.strategy'

describe('JwtStrategy', () => {
  let sut: JwtStrategy
  let config: ConfigService

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
      providers: [JwtStrategy]
    }).compile()

    sut = moduleRef.get(JwtStrategy)
    config = moduleRef.get(ConfigService)
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  it('should throw if id is not a string', async () => {
    await expect(() =>
      sut.validate({
        id: 1 as any,
        typ: JwtType.ACCESS,
        iss: config.get('auth.jwt.issuer')
      })
    ).rejects.toThrow('Token contains invalid user info')
  })

  it('should throw if id is not present', async () => {
    await expect(() =>
      sut.validate({
        id: undefined,
        typ: JwtType.ACCESS,
        iss: config.get('auth.jwt.issuer')
      })
    ).rejects.toThrow('Token contains invalid user info')
  })

  it('should throw if claim type is not ACCESS', async () => {
    await expect(() =>
      sut.validate({
        id: uuid.v4(),
        typ: JwtType.INVITE,
        iss: config.get('auth.jwt.issuer')
      })
    ).rejects.toThrow('Token is not a access token')

    await expect(() =>
      sut.validate({
        id: uuid.v4(),
        typ: JwtType.REFRESH,
        iss: config.get('auth.jwt.issuer')
      })
    ).rejects.toThrow('Token is not a access token')
  })

  it('should throw if claim issuer is invalid', async () => {
    await expect(() =>
      sut.validate({
        id: uuid.v4(),
        typ: JwtType.ACCESS,
        iss: 'someinvalidissuername'
      })
    ).rejects.toThrow('Token has invalid issuer')
  })
})
