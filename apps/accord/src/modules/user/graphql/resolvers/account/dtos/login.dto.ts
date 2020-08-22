import { LoginInput } from '@agree/graphql-typedefs'

import { IsEmail, IsNotEmpty, IsString } from 'class-validator'

export class LoginDTO implements LoginInput {
  @IsNotEmpty()
  @IsEmail()
  email: string

  @IsNotEmpty()
  @IsString()
  password: string
}
