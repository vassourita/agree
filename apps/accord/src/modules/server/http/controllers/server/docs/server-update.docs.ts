import { applyDecorators } from '@nestjs/common'
import { ApiBody, ApiConsumes } from '@nestjs/swagger'

import { UpdateServerDTO } from '../dtos/update-server.dto'

export const ServerUpdateDocs = () =>
  applyDecorators(ApiConsumes('multipart/form-data'), ApiBody({ type: UpdateServerDTO }))
