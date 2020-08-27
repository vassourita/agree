import { Injectable, BadRequestException } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

import { IUseCase } from '@shared/protocols/use-case'

@Injectable()
export class DecodeInviteTokenUseCase implements IUseCase<string, string> {
  constructor(private readonly jwtService: JwtService) {}

  async execute(token: string): Promise<string> {
    const payload = await this.jwtService.decode(token)

    if (typeof payload !== 'object') {
      throw new BadRequestException('Invalid token')
    }

    const { serverId } = payload

    return serverId as string
  }
}
