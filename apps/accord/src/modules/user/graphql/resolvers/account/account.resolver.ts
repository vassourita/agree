import { UnauthorizedException, UseGuards } from '@nestjs/common'
import { Resolver, Args, Mutation } from '@nestjs/graphql'

import { CreateUserUseCase } from '@modules/user/use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '@modules/user/use-cases/find-user-by-id/find-user-by-id.use-case'
import { UpdateUserUseCase } from '@modules/user/use-cases/update-user/update-user.use-case'
import { UploadAvatarUseCase } from '@modules/user/use-cases/upload-avatar/upload-avatar.use-case'
import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { GqlJwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { AuthProvider } from '@shared/providers/auth.provider'
import { FileUpload, GraphQLUpload } from 'graphql-upload'

import { FindUserByEmailUseCase } from '../../../use-cases/find-user-by-email/find-user-by-email.use-case'
import { CreateAccountDTO } from './dtos/create-account.dto'
import { LoginDTO } from './dtos/login.dto'
import { UpdateAccountDTO } from './dtos/update-account.dto'

@Resolver()
export class AccountResolver {
  constructor(
    private readonly auth: AuthProvider,
    private readonly createUserUseCase: CreateUserUseCase,
    private readonly updateUserUseCase: UpdateUserUseCase,
    private readonly findUserByIdUseCase: FindUserByIdUseCase,
    private readonly uploadAvatarUseCase: UploadAvatarUseCase,
    private readonly findUserByEmailUseCase: FindUserByEmailUseCase
  ) {}

  @Mutation()
  async login(@Args('data') data: LoginDTO) {
    const user = await this.findUserByEmailUseCase.execute({ email: data.email })

    if (!(await this.auth.comparePassword(data.password, user.password))) {
      throw new UnauthorizedException('Wrong password')
    }

    const token = await this.auth.signToken(user.id)

    return {
      token
    }
  }

  @Mutation()
  async createAccount(@Args('data') data: CreateAccountDTO) {
    const user = await this.createUserUseCase.execute(data)
    const token = await this.auth.signToken(user.id)

    return {
      user,
      token
    }
  }

  @Mutation()
  @UseGuards(GqlJwtAuthGuard)
  async updateAccount(@Args('data') data: UpdateAccountDTO, @CurrentUserId() id: string) {
    const user = await this.updateUserUseCase.execute({
      ...data,
      id
    })

    return user
  }

  @Mutation()
  @UseGuards(GqlJwtAuthGuard)
  async updateAvatar(
    @Args({ name: 'file', type: () => GraphQLUpload }) avatarFile: FileUpload,
    @CurrentUserId() id: string
  ) {
    const filename = await this.uploadAvatarUseCase.execute({
      avatarFile,
      userId: id
    })

    const user = await this.updateUserUseCase.execute({
      id,
      avatar: filename
    })

    return user
  }
}
