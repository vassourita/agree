import { BadRequestException, Injectable } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { PassportStrategy } from '@nestjs/passport'

import { ExtractJwt, Strategy } from 'passport-jwt'

import { IJwtPayloadDTO, JwtType } from './dtos/jwt-payload.dto'

export const JWT_STRATEGY_NAME = 'default-jwt'

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy, JWT_STRATEGY_NAME) {
  constructor(private readonly config: ConfigService) {
    super({
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      ignoreExpiration: false,
      secretOrKey: config.get('auth.key')
    })
  }

  async validate(payload: IJwtPayloadDTO) {
    const { id, typ, iss } = payload
    if (!id || typeof id !== 'string') {
      throw new BadRequestException('Token contains invalid user info')
    }
    if (typ !== JwtType.ACCESS) {
      throw new BadRequestException('Token is not a access token')
    }
    if (iss !== this.config.get('auth.jwt.issuer')) {
      throw new BadRequestException('Token has invalid issuer')
    }
    return { id }
  }
}
