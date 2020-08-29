import { Injectable, BadRequestException } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

import { IUseCase } from '@shared/protocols/use-case'

@Injectable()
export class DecodeInviteTokenUseCase implements IUseCase<string, string> {
  constructor(private readonly jwtService: JwtService) {}

  async execute(token: string): Promise<string> {
    try {
      const payload = this.jwtService.decode(token) as { serverId: string }

      const { serverId } = payload

      return serverId
    } catch (error) {
      throw new BadRequestException('Invalid token')
    }
  }
}
