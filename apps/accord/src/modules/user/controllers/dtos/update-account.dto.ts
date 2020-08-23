import { IsString } from 'class-validator'

export class UpdateAccountDTO {
  @IsString()
  name: string

  @IsString()
  status: string
}
