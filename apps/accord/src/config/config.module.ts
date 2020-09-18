import { Module } from '@nestjs/common'
import { ConfigModule } from '@nestjs/config'

import { apiConfig } from './modules/api.config'
import { authConfig } from './modules/auth.config'
import { databaseConfig } from './modules/database.config'
import { uploadConfig } from './modules/upload.config'

@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      load: [databaseConfig, apiConfig, authConfig, uploadConfig]
    })
  ]
})
export class AccordConfigModule {}
