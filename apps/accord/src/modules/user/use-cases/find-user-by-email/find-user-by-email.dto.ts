import { IsEmail } from 'class-validator'

export class IFindUserByEmailDTO {
  @IsEmail()
  email: string
}
