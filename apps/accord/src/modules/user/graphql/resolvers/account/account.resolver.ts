import { UnauthorizedException, UseGuards, InternalServerErrorException } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { Resolver, Args, Mutation } from '@nestjs/graphql'

import { CreateUserUseCase } from '@modules/user/use-cases/create-user/create-user.use-case'
import { FindUserByIdUseCase } from '@modules/user/use-cases/find-user-by-id/find-user-by-id.use-case'
import { UpdateUserUseCase } from '@modules/user/use-cases/update-user/update-user.use-case'
import { CurrentUserId } from '@shared/guards/jwt/jwt-autheticated-user.decorator'
import { GqlJwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { AuthProvider } from '@shared/providers/auth.provider'
import { createWriteStream } from 'fs'
import { FileUpload, GraphQLUpload } from 'graphql-upload'
import { join } from 'path'

import { FindUserByEmailUseCase } from '../../../use-cases/find-user-by-email/find-user-by-email.use-case'
import { CreateAccountDTO } from './dtos/create-account.dto'
import { LoginDTO } from './dtos/login.dto'
import { UpdateAccountDTO } from './dtos/update-account.dto'

@Resolver()
export class AccountResolver {
  constructor(
    private readonly createUser: CreateUserUseCase,
    private readonly updateUser: UpdateUserUseCase,
    private readonly auth: AuthProvider,
    private readonly findUserByEmail: FindUserByEmailUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly config: ConfigService
  ) {}

  @Mutation()
  async login(@Args('data') data: LoginDTO) {
    const user = await this.findUserByEmail.execute({ email: data.email })

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
    const user = await this.createUser.execute(data)
    const token = await this.auth.signToken(user.id)

    return {
      user,
      token
    }
  }

  @Mutation()
  @UseGuards(GqlJwtAuthGuard)
  async updateAccount(@Args('data') data: UpdateAccountDTO, @CurrentUserId() id: string) {
    const user = await this.updateUser.execute({
      ...data,
      id
    })

    return user
  }

  @Mutation()
  @UseGuards(GqlJwtAuthGuard)
  async uploadAvatar(
    @Args({ name: 'file', type: () => GraphQLUpload }) avatarFile: FileUpload,
    @CurrentUserId() id: string
  ) {
    const filename = `${Date.now()}-${id}-avatar-${avatarFile.filename}`

    await new Promise((resolve, reject) =>
      avatarFile
        .createReadStream()
        .pipe(createWriteStream(join(this.config.get('upload.dir'), filename)))
        .on('finish', () => resolve(true))
        .on('error', _err => reject(new InternalServerErrorException('Error while uploading the avatar file')))
    )

    const user = await this.updateUser.execute({
      id,
      avatar: filename
    })

    return user
  }
}
