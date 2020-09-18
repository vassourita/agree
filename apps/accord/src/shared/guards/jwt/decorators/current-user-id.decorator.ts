import { createParamDecorator, ExecutionContext, UnauthorizedException } from '@nestjs/common'

import { Request } from 'express'

export const CurrentUserId = createParamDecorator((_, context: ExecutionContext) => {
  const id = context.switchToHttp().getRequest<Request>().user?.id
  if (!id) {
    throw new UnauthorizedException()
  }
  return id
})
