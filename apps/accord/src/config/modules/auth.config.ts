import { registerAs } from '@nestjs/config'

import { randomBytes } from 'crypto'
import { join } from 'path'

export const authConfig = registerAs('auth', () => ({
  key: process.env.API_KEY || 'mysecretkey',
  jwt: {
    expiresIn: '1d',
    saltRounds: 8,
    refreshKey: randomBytes(16).toString('hex'),
    issuer: require(join(process.cwd(), 'package.json')).name
  }
}))
