import { PipeTransform, ArgumentMetadata, BadRequestException } from '@nestjs/common'
import { JwtService } from '@nestjs/jwt'

export class ParseInviteTokenPipe implements PipeTransform {
  constructor(private readonly jwtService: JwtService) {}

  transform(value: any, _metadata: ArgumentMetadata) {
    if (typeof value !== 'string') {
      throw new BadRequestException('Invalid token')
    }

    const payload = this.jwtService.decode(value)

    if (typeof payload !== 'object') {
      throw new BadRequestException('Invalid token')
    }

    const { serverId } = payload

    return serverId as string
  }
}
