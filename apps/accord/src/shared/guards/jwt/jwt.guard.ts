import { Injectable, ExecutionContext, UnauthorizedException } from '@nestjs/common'
import { AuthGuard } from '@nestjs/passport'

import { TokenExpiredError } from 'jsonwebtoken'

import { JWT_STRATEGY_NAME } from './jwt.strategy'

@Injectable()
export class JwtAuthGuard extends AuthGuard(JWT_STRATEGY_NAME) {
  canActivate(context: ExecutionContext) {
    return super.canActivate(context)
  }

  handleRequest(err, user, info, _context) {
    if (info instanceof TokenExpiredError) {
      throw new UnauthorizedException('Access token has expired')
    }

    if (err || !user) {
      throw err || new UnauthorizedException()
    }

    return user
  }
}
