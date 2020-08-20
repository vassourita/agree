import { PipeTransform, ArgumentMetadata, BadRequestException } from '@nestjs/common'

export class ParseNametagPipe implements PipeTransform {
  transform(value: any, _metadata: ArgumentMetadata) {
    if (typeof value !== 'string') {
      throw new BadRequestException('Invalid nametag')
    }

    const parts = value.split('#')
    if (parts.length !== 2) {
      throw new BadRequestException('Nametag malformatted')
    }

    const [name, tag] = parts

    if (isNaN(Number(tag))) {
      throw new BadRequestException('Invalid tag number')
    }

    return [name, tag]
  }
}
