import { Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

import { IUseCase } from '@shared/protocols/use-case'

import { ISignInviteTokenDTO } from './sign-invite-token.dto'

@Injectable()
export class SignInviteTokenUseCase implements IUseCase<ISignInviteTokenDTO, string> {
  constructor(private readonly jwtService: JwtService) {}

  async execute({ serverId, expiresIn }: ISignInviteTokenDTO): Promise<string> {
    const token = await this.jwtService.signAsync({ serverId }, { expiresIn })

    return token
  }
}
