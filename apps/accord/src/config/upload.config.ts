import { BadRequestException } from '@nestjs/common'
import { registerAs } from '@nestjs/config'

import { diskStorage } from 'multer'
import { resolve, extname } from 'path'
import { uuid } from 'uuidv4'

export const uploadConfig = registerAs('upload', () => ({
  dir: resolve(__dirname, '..', '..', 'uploads'),
  storage: diskStorage({
    filename: (req, file, cb) => {
      const randomName = uuid()
      return cb(null, `${randomName}${extname(file.originalname)}`)
    }
  }),
  filter: (req, file: Express.Multer.File, cb) => {
    if (!file.originalname.match(/\.(jpg|jpeg|png|gif)$/)) {
      return cb(new BadRequestException('Uploaded avatar file is not an image'), false)
    }
    return cb(null, true)
  }
}))
