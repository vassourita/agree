import { ValidationPipe } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { NestFactory } from '@nestjs/core'
import { NestExpressApplication } from '@nestjs/platform-express'

import { AppModule } from './shared/app.module'

async function main() {
  const app = await NestFactory.create<NestExpressApplication>(AppModule, {
    cors: true
  })
  app.useGlobalPipes(new ValidationPipe())

  const port = app.get(ConfigService).get('api.port')
  await app.listen(port)
}
main()
