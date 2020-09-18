import { createParamDecorator, ExecutionContext, UnauthorizedException } from '@nestjs/common'

import { UserEntity } from '@modules/user/entities/user.entity'
import { Request } from 'express'
import { getRepository } from 'typeorm'

export const CurrentUser = createParamDecorator(async (_, context: ExecutionContext) => {
  const id = context.switchToHttp().getRequest<Request>().user?.id
  if (!id) {
    throw new UnauthorizedException()
  }

  const user = await getRepository(UserEntity).findOne(id)
  if (!user) {
    throw new UnauthorizedException()
  }

  return user
})
