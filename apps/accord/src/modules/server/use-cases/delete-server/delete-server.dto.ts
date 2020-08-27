import { ServerEntity } from '@modules/server/entities/server.entity'

export interface IDeleteServerDTO {
  userId: string
  server: ServerEntity
}
