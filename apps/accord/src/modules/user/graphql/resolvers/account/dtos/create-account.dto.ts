import { CreateAccountInput } from '@agree/graphql-typedefs'

import { IsNotEmpty, IsEmail, IsString } from 'class-validator'

export class CreateAccountDTO implements CreateAccountInput {
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
