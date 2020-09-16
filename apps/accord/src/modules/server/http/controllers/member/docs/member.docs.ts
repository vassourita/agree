import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger'

export const MemberDocs = () => applyDecorators(ApiTags('members'), ApiBearerAuth())
