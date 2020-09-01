import { ApiProperty } from '@nestjs/swagger'

import { IsString, IsNotEmpty, Matches } from 'class-validator'

export class CreateInviteDTO {
  @IsString()
  @IsNotEmpty()
  @ApiProperty({ format: 'uuid' })
  serverId: string

  @Matches(/[1-9][y|d|h|m|s]/g)
  @IsString()
  @IsNotEmpty()
  @ApiProperty({ example: '7d' })
  expiresIn: string
}
