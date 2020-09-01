import { ValidationPipe } from '@nestjs/common'
import { ConfigService } from '@nestjs/config'
import { NestFactory } from '@nestjs/core'
import { NestExpressApplication } from '@nestjs/platform-express'
import { SwaggerModule, DocumentBuilder } from '@nestjs/swagger'

import { AppModule } from './shared/app.module'

async function main() {
  const app = await NestFactory.create<NestExpressApplication>(AppModule, {
    cors: true
  })
  app.useGlobalPipes(new ValidationPipe())

  const options = new DocumentBuilder()
    .setTitle('Accord')
    .setDescription('Welcome to the Accord API docs!')
    .setVersion('1.0')
    .addBearerAuth()
    .build()
  const document = SwaggerModule.createDocument(app, options)
  SwaggerModule.setup('/docs', app, document)

  const port = app.get(ConfigService).get('api.port')
  await app.listen(port)
}
main()
