import { IsUUID } from 'class-validator'
export class IFindUserByIdDTO {
  @IsUUID('4')
  id: string
}
