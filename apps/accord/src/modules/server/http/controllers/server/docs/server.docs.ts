import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger'

export const ServerDocs = () => applyDecorators(ApiTags('servers'), ApiBearerAuth())
