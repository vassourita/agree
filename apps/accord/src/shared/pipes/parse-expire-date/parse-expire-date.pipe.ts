import { PipeTransform, BadRequestException } from '@nestjs/common'

import ms from 'ms'

export class ParseExpireDatePipe implements PipeTransform {
  async transform(value: unknown) {
    if (!value) {
      return undefined
    }

    if (typeof value !== 'string') {
      throw new BadRequestException('Invalid expire date')
    }

    return ms(ms(value))
  }
}
