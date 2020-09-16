import { ApiProperty } from '@nestjs/swagger'

import { IsNotEmpty, IsEmail, IsString } from 'class-validator'

export class CreateAccountDTO {
  @IsNotEmpty()
  @IsString()
  name: string

  @IsNotEmpty()
  @IsEmail()
  @ApiProperty({ format: 'email' })
  email: string

  @IsNotEmpty()
  @IsString()
  @ApiProperty()
  password: string
}
