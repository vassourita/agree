import { applyDecorators } from '@nestjs/common'
import { ApiBearerAuth, ApiConsumes } from '@nestjs/swagger'

export const UserUpdateDocs = () => applyDecorators(ApiBearerAuth(), ApiConsumes('multipart/form-data'))
