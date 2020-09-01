import { ApiProperty, ApiHideProperty } from '@nestjs/swagger'

import { UserEntity } from '@modules/user/entities/user.entity'
import { Entity, CreateDateColumn, JoinColumn, PrimaryColumn, ManyToOne } from 'typeorm'

import { ServerEntity } from './server.entity'

@Entity('server_member')
export class ServerMemberEntity {
  @PrimaryColumn({ name: 'member_id', type: 'uuid' })
  @ApiProperty({ format: 'uuid' })
  memberId: string

  @PrimaryColumn({ name: 'server_id', type: 'uuid' })
  @ApiProperty({ format: 'uuid' })
  serverId: string

  @CreateDateColumn({ name: 'created_at' })
  createdAt: Date

  constructor(partial: Partial<ServerMemberEntity>) {
    Object.assign(this, partial)
  }

  @ManyToOne(_type => UserEntity, user => user.serverMembers, { onDelete: 'CASCADE' })
  @JoinColumn({ name: 'member_id', referencedColumnName: 'id' })
  @ApiHideProperty()
  member: UserEntity

  @ManyToOne(_type => ServerEntity, server => server.serverMembers, { onDelete: 'CASCADE' })
  @JoinColumn({ name: 'server_id', referencedColumnName: 'id' })
  @ApiHideProperty()
  server: ServerEntity
}
