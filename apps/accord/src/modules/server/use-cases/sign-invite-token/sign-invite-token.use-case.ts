import { Injectable } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

import { IUseCase } from '@shared/protocols/use-case'

@Injectable()
export class SignInviteTokenUseCase implements IUseCase<string, string> {
  constructor(private readonly jwtService: JwtService) {}

  async execute(serverId: string): Promise<string> {
    const token = await this.jwtService.signAsync({ serverId })

    return token
  }
}
