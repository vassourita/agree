import { applyDecorators } from '@nestjs/common'
import { ApiProperty, ApiBody, ApiResponse } from '@nestjs/swagger'

class SessionRefreshBody {
  @ApiProperty({ example: 'somejwtbearerrefreshtoken' })
  refreshToken: string
}

class SessionRefreshResponse {
  @ApiProperty({ example: 'somejwtbeareraccesstoken' })
  accessToken: string
}

export const SessionRefreshDocs = () =>
  applyDecorators(ApiBody({ type: SessionRefreshBody }), ApiResponse({ type: SessionRefreshResponse }))
