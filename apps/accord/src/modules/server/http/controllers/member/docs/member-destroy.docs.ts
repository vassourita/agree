import { applyDecorators } from '@nestjs/common'
import { ApiBody } from '@nestjs/swagger'

export const MemberDestroyDocs = () =>
  applyDecorators(ApiBody({ schema: { example: { memberId: 'the user id to be removed from the server' } } }))
