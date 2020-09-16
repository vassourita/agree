import { applyDecorators } from '@nestjs/common'
import { ApiBody, ApiResponse } from '@nestjs/swagger'

import { CreateInviteDTO } from '../dtos/create-invite.dto'

export const InviteGenerateDocs = () =>
  applyDecorators(
    ApiBody({ type: CreateInviteDTO }),
    ApiResponse({ schema: { example: { token: 'somebearerinvitetoken' } } })
  )
