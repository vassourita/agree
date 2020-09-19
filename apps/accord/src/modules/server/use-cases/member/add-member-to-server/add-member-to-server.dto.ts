import { ServerEntity } from '@modules/server/entities/server.entity'

export interface IAddMemberToServerDTO {
  userId: string
  server: ServerEntity
}
