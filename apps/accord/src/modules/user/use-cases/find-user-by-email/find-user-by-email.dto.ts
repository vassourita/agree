import { IsEmail } from 'class-validator'

export class FindUserByEmailDTO {
  @IsEmail()
  email: string
}
