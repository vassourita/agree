import { Injectable } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { JwtService } from '@nestjs/jwt'

import { compare, hash } from 'bcryptjs'

@Injectable()
export class AuthProvider {
  constructor(private readonly jwtService: JwtService, private readonly config: ConfigService) {}

  comparePassword(password: string, hashed: string) {
    return compare(password, hashed)
  }

  hashPassword(password: string) {
    const saltRounds = this.config.get('auth.jwt.saltRounds')
    return hash(password, saltRounds)
  }

  signToken(id: string) {
    return this.jwtService.signAsync({ id })
  }
}
