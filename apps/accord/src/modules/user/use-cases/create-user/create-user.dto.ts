import { CreateUserInput } from '@agree/graphql-typedefs'

import { IsString, IsEmail } from 'class-validator'

export class ICreateUserDTO implements CreateUserInput {
  @IsString()
  name: string

  @IsEmail()
  email: string

  @IsString()
  password: string
}
