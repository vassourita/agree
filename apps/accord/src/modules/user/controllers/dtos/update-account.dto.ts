import { ApiPropertyOptional } from '@nestjs/swagger'

import { IsString, IsNotEmpty } from 'class-validator'

export class UpdateAccountDTO {
  @IsString()
  @IsNotEmpty()
  @ApiPropertyOptional()
  name: string

  @IsString()
  @IsNotEmpty()
  @ApiPropertyOptional()
  status: string

  @ApiPropertyOptional({ type: 'string', format: 'binary' })
  avatar: Express.Multer.File
}
