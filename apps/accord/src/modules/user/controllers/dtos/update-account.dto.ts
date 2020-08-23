import { IsString, IsNotEmpty } from 'class-validator'

export class UpdateAccountDTO {
  @IsString()
  @IsNotEmpty()
  name: string

  @IsString()
  @IsNotEmpty()
  status: string
}
