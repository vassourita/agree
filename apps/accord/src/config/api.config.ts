import { registerAs } from '@nestjs/config'

export const apiConfig = registerAs('api', () => ({
  port: process.env.PORT || 3333,
  env: process.env.NODE_ENV || 'development'
}))
