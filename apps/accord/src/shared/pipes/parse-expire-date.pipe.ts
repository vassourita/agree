import { PipeTransform, ArgumentMetadata, BadRequestException } from '@nestjs/common'

import ms from 'ms'

export class ParseExpireDatePipe implements PipeTransform {
  transform(value: any, _metadata: ArgumentMetadata) {
    if (!value) {
      return undefined
    }

    if (typeof value !== 'string') {
      throw new BadRequestException('Invalid expire date')
    }

    return ms(ms(value))
  }
}
