import { ApiPropertyOptional } from '@nestjs/swagger'

import { IsNotEmpty, IsString } from 'class-validator'

export class UpdateChannelDTO {
  @IsString()
  @IsNotEmpty()
  @ApiPropertyOptional({ example: 'new name for my channel' })
  name?: string

  @IsString()
  @IsNotEmpty()
  @ApiPropertyOptional({ example: 'some other category' })
  category?: string
}
