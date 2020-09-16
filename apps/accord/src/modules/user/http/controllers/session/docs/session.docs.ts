import { applyDecorators } from '@nestjs/common'
import { ApiTags } from '@nestjs/swagger'

export const SessionDocs = () => applyDecorators(ApiTags('sessions'))
