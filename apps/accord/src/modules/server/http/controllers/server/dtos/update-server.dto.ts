import { IsString, IsNotEmpty } from 'class-validator'

export class UpdateServerDTO {
  @IsString()
  @IsNotEmpty()
  name: string
}
