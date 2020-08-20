import { Module } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { TypeOrmModule } from '@nestjs/typeorm'

import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'

import { UserEntity } from './entities/user.entity'
import { AuthResolver } from './graphql/resolvers/auth.resolver'
import { UserResolver } from './graphql/resolvers/user.resolver'
import { userUseCases } from './use-cases'

@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity]),
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
  providers: [UserResolver, AuthResolver, AuthProvider, JwtStrategy, ...userUseCases]
})
export class UserModule {}
