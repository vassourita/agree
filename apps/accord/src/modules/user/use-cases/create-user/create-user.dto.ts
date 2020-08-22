import { CreateAccountInput } from '@agree/graphql-typedefs'

import { IsString, IsEmail } from 'class-validator'

export class CreateUserDTO implements CreateAccountInput {
  @IsString()
  name: string

  @IsEmail()
  email: string

  @IsString()
  password: string
}
