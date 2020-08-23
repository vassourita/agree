import { IsString, IsEmail } from 'class-validator'

export class CreateUserDTO {
  @IsString()
  name: string

  @IsEmail()
  email: string

  @IsString()
  password: string
}
