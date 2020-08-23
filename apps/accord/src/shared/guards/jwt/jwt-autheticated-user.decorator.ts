import { createParamDecorator, ExecutionContext, UnauthorizedException } from '@nestjs/common'

export const CurrentUserId = createParamDecorator((_, context: ExecutionContext) => {
  const id = context.switchToHttp().getRequest().user?.id
  if (!id) {
    throw new UnauthorizedException()
  }
  return id
})
