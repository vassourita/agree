import { Module } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtModule } from '@nestjs/jwt'
import { TypeOrmModule } from '@nestjs/typeorm'

import { JwtStrategy } from 'src/shared/guards/jwt/jwt.strategy'
import { AuthProvider } from 'src/shared/providers/auth.provider'

import { UserEntity } from './entities/user.entity'
import { AuthResolver } from './graphql/resolvers/auth.resolver'
import { UserResolver } from './graphql/resolvers/user.resolver'
import { CreateUserUseCase } from './use-cases/create-user/create-user.use-case'
import { FindUserByEmailUseCase } from './use-cases/find-user-by-email/find-user-by-email.use-case'
import { FindUserByIdUseCase } from './use-cases/find-user-by-id/find-user-by-id.use-case'
import { ListUsersUseCase } from './use-cases/list-users/list-users.use-case'

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
  providers: [
    UserResolver,
    AuthResolver,
    CreateUserUseCase,
    ListUsersUseCase,
    FindUserByIdUseCase,
    FindUserByEmailUseCase,
    AuthProvider,
    JwtStrategy
  ]
})
export class UserModule {}
