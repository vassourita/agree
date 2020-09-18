import { applyDecorators } from '@nestjs/common'
import { ApiBody } from '@nestjs/swagger'

export const InviteShowDocs = () =>
  applyDecorators(ApiBody({ schema: { example: { token: 'somebearerinvitetoken' } } }))
