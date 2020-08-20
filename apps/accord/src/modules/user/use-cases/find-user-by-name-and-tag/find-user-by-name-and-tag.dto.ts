import { IsString, IsNumberString, IsNotEmpty } from 'class-validator'

export class FindUserByNameAndTagDTO {
  @IsString()
  @IsNotEmpty()
  name: string

  @IsNumberString()
  @IsNotEmpty()
  tag: number
}
