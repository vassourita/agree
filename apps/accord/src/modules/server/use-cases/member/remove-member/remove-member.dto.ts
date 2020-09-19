import { ServerEntity } from '@modules/server/entities/server.entity'

export interface IRemoveMemberDTO {
  memberId: string
  loggedUserId: string
  server: ServerEntity
}
