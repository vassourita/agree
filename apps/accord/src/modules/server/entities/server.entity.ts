import { UserEntity } from '@modules/user/entities/user.entity'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, JoinColumn, ManyToOne, OneToMany } from 'typeorm'

import { ServerMemberEntity } from './server-member.entity'

@Entity('server')
export class ServerEntity {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column()
  name: string

  @Column({ name: 'member_count', type: 'int', default: 0 })
  memberCount: number

  @Column({ name: 'owner_id', type: 'uuid' })
  ownerId: string

  @CreateDateColumn({ name: 'created_at' })
  createdAt: Date

  constructor(partial: Partial<ServerEntity>) {
    Object.assign(this, partial)
  }

  @ManyToOne(_type => UserEntity, user => user.ownedServers)
  @JoinColumn({ name: 'owner_id', referencedColumnName: 'id' })
  owner: UserEntity

  @OneToMany(_type => ServerMemberEntity, s => s.server, { onDelete: 'CASCADE' })
  serverMembers: ServerMemberEntity[]
}
