import { applyDecorators } from '@nestjs/common'
import { ApiProperty, ApiResponse } from '@nestjs/swagger'

class SessionStoreResponse {
  @ApiProperty({ example: 'somejwtbeareraccesstoken' })
  accessToken: string

  @ApiProperty({ example: 'somejwtbearerrefreshtoken' })
  refreshToken: string
}

export const SessionStoreDocs = () => applyDecorators(ApiResponse({ type: SessionStoreResponse }))
