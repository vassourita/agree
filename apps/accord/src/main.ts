import { ConfigService } from '@nestjs/config'
import { NestFactory } from '@nestjs/core'

import { AppModule } from './shared/app.module'

async function main() {
  const app = await NestFactory.create(AppModule, {
    cors: true
  })
  const configService = app.get(ConfigService)

  const port = configService.get('api.port')
  await app.listen(port)
}
main()
