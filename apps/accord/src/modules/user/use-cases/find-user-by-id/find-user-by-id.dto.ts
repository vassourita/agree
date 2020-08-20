import { IsUUID } from 'class-validator'
export class FindUserByIdDTO {
  @IsUUID('4')
  id: string
}
