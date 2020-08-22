import { registerAs } from '@nestjs/config'

import { resolve } from 'path'

export const uploadConfig = registerAs('upload', () => ({
  dir: resolve(__dirname, '..', '..', 'uploads')
}))
