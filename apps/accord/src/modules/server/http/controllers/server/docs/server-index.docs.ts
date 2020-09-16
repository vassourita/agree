import { applyDecorators } from '@nestjs/common'
import { ApiQuery } from '@nestjs/swagger'

export const ServerIndexDocs = () =>
  applyDecorators(
    ApiQuery({ name: 'name', required: false, type: 'string' }),
    ApiQuery({ name: 'page', required: false, type: 'integer' }),
    ApiQuery({ name: 'limit', required: false, type: 'integer' })
  )
