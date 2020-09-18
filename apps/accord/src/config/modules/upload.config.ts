import { BadRequestException } from '@nestjs/common'
import { registerAs } from '@nestjs/config'

import { diskStorage } from 'multer'
import { resolve, extname } from 'path'
import * as uuid from 'uuid'

export const uploadConfig = registerAs('upload', () => ({
  dir: resolve(__dirname, '..', '..', 'uploads'),
  storage: diskStorage({
    filename: (_req, file, cb) => {
      const randomName = uuid.v4()
      return cb(null, `${randomName}${extname(file.originalname)}`)
    },
    destination: resolve(__dirname, '..', '..', 'uploads')
  }),
  filter: (_req, file: Express.Multer.File, cb) => {
    const ext = extname(file.originalname)
    if (!ext.match(/(.jpg|.jpeg|.png|.gif)$/)) {
      return cb(new BadRequestException('Uploaded avatar file is not an image'), false)
    }
    return cb(null, true)
  }
}))