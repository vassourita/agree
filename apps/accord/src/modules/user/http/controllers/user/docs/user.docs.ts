import { applyDecorators } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'

export const UserDocs = () => applyDecorators(ApiTags('users'))
