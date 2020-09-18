import { Injectable, BadRequestException } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

import { IJwtPayloadDTO, JwtType } from '@shared/guards/jwt/dtos/jwt-payload.dto'
import { IUseCase } from '@shared/protocols/use-case'

@Injectable()
export class DecodeInviteTokenUseCase implements IUseCase<string, string> {
  constructor(private readonly jwtService: JwtService) {}

  async execute(token: string): Promise<string> {
    try {
      const payload = this.jwtService.decode(token) as IJwtPayloadDTO

      const { id, typ } = payload

      if (typ !== JwtType.INVITE) {
        throw new BadRequestException('Token is not a invite token')
      }

      return id
    } catch (error) {
      if (error instanceof BadRequestException) throw error

      throw new BadRequestException('Invalid token')
    }
  }
}
