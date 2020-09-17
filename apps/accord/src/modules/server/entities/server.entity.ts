import { ApiProperty, ApiHideProperty } from '@nestjs/swagger'

import { ChannelEntity } from '@modules/channel/entities/channel.entity'
import { UserEntity } from '@modules/user/entities/user.entity'
import { Expose } from 'class-transformer'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, JoinColumn, ManyToOne, OneToMany } from 'typeorm'

import { ServerMemberEntity } from './server-member.entity'

@Entity('server')
export class ServerEntity {
  @PrimaryGeneratedColumn('uuid')
  @ApiProperty({ format: 'uuid' })
  id: string

  @Column()
  name: string

  @Column({ name: 'member_count', type: 'int', default: 0 })
  memberCount: number

  @Column({ name: 'owner_id', type: 'uuid' })
  @ApiProperty({ format: 'uuid' })
  ownerId: string

  @CreateDateColumn({ name: 'created_at' })
  createdAt: Date

  @Column({ nullable: true })
  @ApiProperty({ example: 'someserverimage.png' })
  avatar: string

  @Expose()
  @ApiProperty({ example: 'http://localhost:4001/files/someserverimage.png' })
  get avatarUrl() {
    return this.avatar ? `${process.env.PUBLIC_FILES_URL}/${this.avatar}` : null
  }

  constructor(partial: Partial<ServerEntity>) {
    Object.assign(this, partial)
  }

  @ManyToOne(_type => UserEntity, user => user.ownedServers)
  @JoinColumn({ name: 'owner_id', referencedColumnName: 'id' })
  @ApiHideProperty()
  owner: UserEntity

  @OneToMany(_type => ServerMemberEntity, s => s.server, { onDelete: 'CASCADE' })
  @ApiHideProperty()
  serverMembers: ServerMemberEntity[]

  @OneToMany(_type => ChannelEntity, channel => channel.server, { onDelete: 'CASCADE' })
  @ApiHideProperty()
  channels: ChannelEntity[]
}
