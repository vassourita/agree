import { ServerEntity } from '@modules/server/entities/server.entity'

export interface IUpdateServerDTO {
  server: ServerEntity
  update: {
    name: string
  }
}
