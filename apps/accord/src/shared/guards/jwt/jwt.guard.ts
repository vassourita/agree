import { Injectable, ExecutionContext, UnauthorizedException } from '@nestjs/common'
import { AuthGuard } from '@nestjs/passport'

import { Request } from 'express'
import { TokenExpiredError } from 'jsonwebtoken'

import { JWT_STRATEGY_NAME } from './jwt.strategy'

@Injectable()
export class JwtAuthGuard extends AuthGuard(JWT_STRATEGY_NAME) {
  canActivate(context: ExecutionContext) {
    const userId = context.switchToHttp().getRequest<Request>().user?.id
    if (!userId) {
      throw new UnauthorizedException('Authorization not found')
    }

    return super.canActivate(context)
  }

  handleRequest(err, user, info, _context) {
    if (info instanceof TokenExpiredError) {
      throw new UnauthorizedException('Access token has expired. Please login again')
    }

    if (err || !user) {
      throw err || new UnauthorizedException('Internal server error')
    }

    return user
  }
}
