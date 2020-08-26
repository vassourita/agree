import { UserEntity } from '@modules/user/entities/user.entity'
import { Entity, CreateDateColumn, JoinColumn, PrimaryColumn, ManyToOne } from 'typeorm'

import { ServerEntity } from './server.entity'

@Entity('server_member')
export class ServerMemberEntity {
  @PrimaryColumn({ name: 'member_id', type: 'uuid' })
  memberId: string

  @PrimaryColumn({ name: 'server_id', type: 'uuid' })
  serverId: string

  @CreateDateColumn({ name: 'created_at', type: 'timestamp with time zone' })
  createdAt: Date

  constructor(partial: Partial<ServerMemberEntity>) {
    Object.assign(this, partial)
  }

  @ManyToOne(_type => UserEntity)
  @JoinColumn({ name: 'member_id', referencedColumnName: 'id' })
  member: UserEntity

  @ManyToOne(_type => ServerEntity)
  @JoinColumn({ name: 'server_id', referencedColumnName: 'id' })
  server: ServerEntity
}
