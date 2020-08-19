import { registerAs } from '@nestjs/config'

export const databaseConfig = registerAs('database', () => ({
  development: {
    type: 'postgres',
    host: process.env.DATABASE_HOST || 'localhost',
    port: Number(process.env.DATABASE_PORT) || 5432,
    database: process.env.DATABASE_NAME || 'accord',
    username: process.env.DATABASE_USER || 'docker',
    password: process.env.DATABASE_PASS || 'docker',
    autoLoadEntities: true
  },
  test: {
    type: 'sqlite',
    database: ':memory:',
    autoLoadEntities: true
  }
}))
