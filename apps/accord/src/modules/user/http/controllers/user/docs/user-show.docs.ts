import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiParam } from '@nestjs/swagger'

export const UserShowDocs = () =>
  applyDecorators(ApiBearerAuth(), ApiParam({ name: 'nametag', example: 'Vassoura#8230', type: 'string' }))
