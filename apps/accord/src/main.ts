import { NestFactory } from '@nestjs/core'

import { AppModule } from './shared/app.module'

async function main() {
  const app = await NestFactory.create(AppModule, {
    cors: true
  })

  const port = process.env.PORT || 3333
  await app.listen(port)
}
main()
