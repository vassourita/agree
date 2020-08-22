import { UpdateAccountInput } from '@agree/graphql-typedefs'

import { IsString } from 'class-validator'

export class UpdateAccountDTO implements UpdateAccountInput {
  @IsString()
  name: string

  @IsString()
  status: string
}
