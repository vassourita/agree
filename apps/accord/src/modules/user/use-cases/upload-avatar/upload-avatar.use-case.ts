import { Injectable, InternalServerErrorException, BadRequestException } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { InjectRepository } from '@nestjs/typeorm'

import { IUseCase } from '@shared/protocols/use-case'
import fs from 'fs'
import path from 'path'
import { Repository } from 'typeorm'

import { UserEntity } from '../../entities/user.entity'
import { UploadAvatarDTO } from './upload-avatar.dto'

@Injectable()
export class UploadAvatarUseCase implements IUseCase<UploadAvatarDTO, string> {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>,
    private readonly config: ConfigService
  ) {}

  async execute(data: UploadAvatarDTO) {
    const accepted = ['image/gif', 'image/jpeg', 'image/png']
    if (!accepted.includes(data.avatarFile.mimetype)) {
      throw new BadRequestException('Uploaded file is not an image')
    }

    const filePath = this.config.get('upload.dir')
    const filename = `${data.userId}-avatar${path.extname(data.avatarFile.filename)}`

    await new Promise((resolve, reject) =>
      data.avatarFile
        .createReadStream()
        .pipe(fs.createWriteStream(path.join(filePath, filename)))
        .on('finish', () => resolve(true))
        .on('error', _err => reject(new InternalServerErrorException('Error while uploading the avatar file')))
    )

    return filename
  }
}
