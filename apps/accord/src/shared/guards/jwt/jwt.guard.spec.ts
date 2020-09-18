import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { Test } from '@nestjs/testing'
import { TypeOrmModule } from '@nestjs/typeorm'

import { AccordConfigModule } from '@config/config.module'
import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { DatabaseModule } from '@shared/database/database.module'
import { TokenExpiredError } from 'jsonwebtoken'

import { JwtAuthGuard } from './jwt.guard'
import { JwtStrategy } from './jwt.strategy'

describe('JwtAuthGuard', () => {
  let sut: JwtAuthGuard

  const mockExecutionContext = {
    switchToHttp: jest.fn().mockReturnThis(),
    getRequest: jest.fn().mockReturnThis()
  }

  beforeAll(async () => {
    await Test.createTestingModule({
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
  })

  beforeEach(async () => {
    sut = new JwtAuthGuard()
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
  })

  describe('#canActivate', () => {
    it('should throw if no userId is present', async () => {
      mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
        params: {},
        body: {},
        user: {}
      })
      expect(() => sut.canActivate(mockExecutionContext as any)).toThrow('Authorization not found')

      mockExecutionContext.switchToHttp().getRequest.mockReturnValueOnce({
        params: {},
        body: {},
        user: { id: undefined }
      })
      expect(() => sut.canActivate(mockExecutionContext as any)).toThrow('Authorization not found')
    })
  })

  describe('#handleRequest', () => {
    it('should throw if the access token has expired', async () => {
      const cb = () =>
        sut.handleRequest(null, { id: 1 }, new TokenExpiredError('token expired', new Date()), mockExecutionContext)

      expect(cb).toThrow('Access token has expired. Please login again')
    })

    it('should throw if any error is present', async () => {
      const cb = () => sut.handleRequest(new Error('some validation error'), { id: 1 }, null, mockExecutionContext)

      expect(cb).toThrow('some validation error')
    })

    it('should throw if user is not present', async () => {
      const cb = () => sut.handleRequest(null, null, null, mockExecutionContext)

      expect(cb).toThrow('Internal server error')
    })
  })
})
