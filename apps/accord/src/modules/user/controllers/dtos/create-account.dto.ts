import { IsNotEmpty, IsEmail, IsString } from 'class-validator'

export class CreateAccountDTO {
  @IsNotEmpty()
  @IsString()
  name: string

  @IsNotEmpty()
  @IsEmail()
  email: string

  @IsNotEmpty()
  @IsString()
  password: string
}
