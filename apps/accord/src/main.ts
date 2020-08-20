import { ConfigService } from '@nestjs/config'
import { NestFactory } from '@nestjs/core'

import { AppModule } from './shared/app.module'

async function main() {
  const app = await NestFactory.create(AppModule, {
    cors: true
  })
  const port = app.get(ConfigService).get('api.port')
  await app.listen(port)
}
main()
