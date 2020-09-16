import { applyDecorators } from '@nestjs/common'
import { ApiBody } from '@nestjs/swagger'

export const MemberStoreDocs = () =>
  applyDecorators(ApiBody({ schema: { example: { memberId: 'the user id to be added in the server' } } }))
