import { registerAs } from '@nestjs/config'

export const authConfig = registerAs('auth', () => ({
  key: process.env.API_KEY || 'mysecretkey',
  jwt: {
    expiresIn: '7d',
    saltRounds: 8
  }
}))
