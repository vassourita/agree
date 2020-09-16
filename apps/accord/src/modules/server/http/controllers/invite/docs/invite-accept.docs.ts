import { applyDecorators } from '@nestjs/common'
import { ApiBody } from '@nestjs/swagger'

export const InviteAcceptDocs = () =>
  applyDecorators(ApiBody({ schema: { example: { token: 'somebearerinvitetoken' } } }))
