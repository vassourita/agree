import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger'

export const ChannelDocs = () => applyDecorators(ApiTags('channels'), ApiBearerAuth())
