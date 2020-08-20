import { UpdateAccountInput } from '@agree/graphql-typedefs'

import { IsString, IsUUID } from 'class-validator'

export class UpdateUserDTO implements UpdateAccountInput {
  @IsString()
  name?: string

  @IsString()
  status?: string

  @IsUUID()
  id: string
}
