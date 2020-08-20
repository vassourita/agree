import { Module } from '@nestjs/common'
import { ConfigModule } from '@nestjs/config'

import { apiConfig } from './api.config'
import { authConfig } from './auth.config'
import { databaseConfig } from './database.config'

@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      load: [databaseConfig, apiConfig, authConfig]
    })
  ]
})
export class AccordConfigModule {}
