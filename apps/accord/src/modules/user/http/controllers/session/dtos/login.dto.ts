import { ApiProperty } from '@nestjs/swagger'

import { IsEmail, IsNotEmpty, IsString } from 'class-validator'

export class LoginDTO {
  @IsNotEmpty()
  @IsEmail()
  @ApiProperty({ format: 'email' })
  email: string

  @IsNotEmpty()
  @IsString()
  password: string
}
