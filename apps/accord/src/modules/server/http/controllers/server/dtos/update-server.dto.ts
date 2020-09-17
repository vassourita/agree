import { ApiPropertyOptional } from '@nestjs/swagger'

import { IsString } from 'class-validator'

export class UpdateServerDTO {
  @IsString()
  @ApiPropertyOptional()
  name: string

  @ApiPropertyOptional({ type: 'string', format: 'binary' })
  avatar: Express.Multer.File
}
