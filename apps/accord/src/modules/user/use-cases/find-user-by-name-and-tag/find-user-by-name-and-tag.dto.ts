import { IsString, IsInt } from 'class-validator'

export class IFindUserByNameAndTagDTO {
  @IsString()
  name: string

  @IsInt()
  tag: number
}
