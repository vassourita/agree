import { IsString, IsUUID } from 'class-validator'

export class UpdateUserDTO {
  @IsString()
  name?: string

  @IsString()
  status?: string

  @IsUUID()
  id: string

  @IsString()
  avatar?: string
}
