import { Module } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { TypeOrmModule } from '@nestjs/typeorm'

import { JwtStrategy } from '@shared/guards/jwt/jwt.strategy'
import { AuthProvider } from '@shared/providers/auth.provider'

import { UserEntity } from './entities/user.entity'
import { resolvers } from './graphql/resolvers'
import { useCases } from './use-cases'

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
  providers: [...resolvers, AuthProvider, JwtStrategy, ...useCases]
})
export class UserModule {}
