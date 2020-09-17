import { Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

import { IJwtPayloadDTO, JwtType } from '@shared/guards/jwt/jwt-payload.dto'
import { IUseCase } from '@shared/protocols/use-case'

import { ISignInviteTokenDTO } from './sign-invite-token.dto'

@Injectable()
export class SignInviteTokenUseCase implements IUseCase<ISignInviteTokenDTO, string> {
  constructor(private readonly jwtService: JwtService) {}

  async execute({ serverId, expiresIn }: ISignInviteTokenDTO): Promise<string> {
    const payload: IJwtPayloadDTO = {
      id: serverId,
      typ: JwtType.INVITE
    }
    const token = await this.jwtService.signAsync(payload, { expiresIn })

    return token
  }
}
