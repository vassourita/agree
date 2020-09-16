import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger'

export const InviteDocs = () => applyDecorators(ApiTags('invites'), ApiBearerAuth())
