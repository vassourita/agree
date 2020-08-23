import { IsNotEmpty, IsEmail, IsString, Length } from 'class-validator'

export class CreateAccountDTO {
  @IsNotEmpty()
  @IsString()
  name: string

  @IsNotEmpty()
  @IsEmail()
  email: string

  @IsNotEmpty()
  @IsString()
  @Length(7)
  password: string
}
