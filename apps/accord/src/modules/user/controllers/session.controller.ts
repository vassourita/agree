import {
  Controller,
  Post,
  Body,
  UnauthorizedException,
  UseInterceptors,
  ClassSerializerInterceptor,
  Put
} from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtService } from '@nestjs/jwt'
import { ApiTags } from '@nestjs/swagger'

import { IJwtPayloadDTO } from '@shared/guards/jwt/jwt-payload.dto'
import { AuthProvider } from '@shared/providers/auth.provider'

import { FindUserByEmailUseCase } from '../use-cases/find-user-by-email/find-user-by-email.use-case'
import { FindUserByIdUseCase } from '../use-cases/find-user-by-id/find-user-by-id.use-case'
import { LoginDTO } from './dtos/login.dto'

@Controller('/sessions')
@UseInterceptors(ClassSerializerInterceptor)
@ApiTags('sessions')
export class SessionController {
  constructor(
    private readonly auth: AuthProvider,
    private readonly findUserByEmail: FindUserByEmailUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly jwtService: JwtService,
    private readonly config: ConfigService
  ) {}

  @Post('/')
  public async store(@Body() data: LoginDTO) {
    const user = await this.findUserByEmail.execute({ email: data.email })

    if (!user) {
      throw new UnauthorizedException('User does not exists')
    }

    if (!(await this.auth.comparePassword(data.password, user.password))) {
      throw new UnauthorizedException('Wrong password')
    }

    const payload: IJwtPayloadDTO = { id: user.id }

    const accessToken = this.jwtService.sign(payload, {
      secret: this.config.get('auth.key'),
      expiresIn: '6h'
    })

    const refreshToken = this.jwtService.sign(payload, {
      secret: this.config.get('auth.jwt.refreshKey'),
      expiresIn: '1y'
    })

    return {
      accessToken,
      refreshToken
    }
  }

  @Put('/')
  async refresh(@Body('refreshToken') refreshToken: string) {
    const { id } = this.jwtService.decode(refreshToken) as IJwtPayloadDTO
    const user = await this.findUserById.execute({ id })

    if (!user) {
      throw new UnauthorizedException('User does not exists')
    }

    const payload: IJwtPayloadDTO = { id: user.id }

    const accessToken = this.jwtService.sign(payload, {
      secret: this.config.get('auth.key'),
      expiresIn: '6h'
    })

    return {
      accessToken
    }
  }
}
