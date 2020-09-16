import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiQuery } from '@nestjs/swagger'

export const UserIndexDocs = () =>
  applyDecorators(
    ApiBearerAuth(),
    ApiQuery({ name: 'page', required: false, type: 'integer' }),
    ApiQuery({ name: 'limit', required: false, type: 'integer' })
  )
