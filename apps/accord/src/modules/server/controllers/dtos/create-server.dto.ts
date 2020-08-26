import { IsString, IsNotEmpty } from 'class-validator'

export class CreateServerDTO {
  @IsString()
  @IsNotEmpty()
  name: string
}
