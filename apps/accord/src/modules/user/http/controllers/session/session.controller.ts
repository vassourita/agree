import {
  Controller,
  Post,
  Body,
  UnauthorizedException,
  UseInterceptors,
  ClassSerializerInterceptor,
  BadRequestException
} from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtService } from '@nestjs/jwt'

import { IJwtPayloadDTO, JwtType } from '@shared/guards/jwt/jwt-payload.dto'
import { AuthProvider } from '@shared/providers/auth.provider'

import { FindUserByEmailUseCase } from '../../../use-cases/find-user-by-email/find-user-by-email.use-case'
import { FindUserByIdUseCase } from '../../../use-cases/find-user-by-id/find-user-by-id.use-case'
import { SessionRefreshDocs } from './docs/session-refresh.docs'
import { SessionStoreDocs } from './docs/session-store.docs'
import { SessionDocs } from './docs/session.docs'
import { LoginDTO } from './dtos/login.dto'

@Controller('/sessions')
@UseInterceptors(ClassSerializerInterceptor)
@SessionDocs()
export class SessionController {
  constructor(
    private readonly auth: AuthProvider,
    private readonly findUserByEmail: FindUserByEmailUseCase,
    private readonly findUserById: FindUserByIdUseCase,
    private readonly jwtService: JwtService,
    private readonly config: ConfigService
  ) {}

  @Post('/')
  @SessionStoreDocs()
  public async store(@Body() data: LoginDTO) {
    const user = await this.findUserByEmail.execute({ email: data.email })

    if (!user) {
      throw new UnauthorizedException('User does not exists')
    }

    if (!(await this.auth.comparePassword(data.password, user.password))) {
      throw new UnauthorizedException('Wrong password')
    }

    const accessPayload: IJwtPayloadDTO = { id: user.id, typ: JwtType.ACCESS }
    const accessToken = this.jwtService.sign(accessPayload, {
      secret: this.config.get('auth.key'),
      expiresIn: '6h'
    })

    const refreshPayload: IJwtPayloadDTO = { id: user.id, typ: JwtType.REFRESH }
    const refreshToken = this.jwtService.sign(refreshPayload, {
      secret: this.config.get('auth.jwt.refreshKey'),
      expiresIn: '1y'
    })

    return {
      accessToken,
      refreshToken
    }
  }

  @Post('/refresh')
  @SessionRefreshDocs()
  async refresh(@Body('refreshToken') refreshToken: string) {
    const { id, typ } = this.jwtService.decode(refreshToken) as IJwtPayloadDTO
    if (typ !== JwtType.REFRESH) {
      throw new BadRequestException('Token is not a refresh token')
    }

    const user = await this.findUserById.execute({ id })
    if (!user) {
      throw new UnauthorizedException('User does not exists')
    }

    const payload: IJwtPayloadDTO = { id: user.id, typ: JwtType.ACCESS }
    const accessToken = this.jwtService.sign(payload, {
      secret: this.config.get('auth.key'),
      expiresIn: '6h'
    })

    return {
      accessToken
    }
  }
}
