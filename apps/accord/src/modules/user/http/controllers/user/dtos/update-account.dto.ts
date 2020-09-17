import { ApiPropertyOptional } from '@nestjs/swagger'

import { IsString } from 'class-validator'

export class UpdateAccountDTO {
  @IsString()
  @ApiPropertyOptional()
  name: string

  @IsString()
  @ApiPropertyOptional()
  status: string

  @ApiPropertyOptional({ type: 'string', format: 'binary' })
  avatar: Express.Multer.File
}
