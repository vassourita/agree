import { ServerEntity } from '@modules/server/entities/server.entity'
import { Entity, PrimaryGeneratedColumn, CreateDateColumn, Column, ManyToOne, JoinColumn } from 'typeorm'

export enum ChannelTypes {
  MEDIA = 'media',
  TEXT = 'text'
}

@Entity('channel')
export class ChannelEntity {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column({ name: 'server_id', type: 'uuid' })
  serverId: string

  @Column()
  name: string

  @Column({
    type: 'enum',
    enum: ChannelTypes,
    default: ChannelTypes.TEXT
  })
  type: ChannelTypes

  @Column()
  category: string

  @CreateDateColumn({ name: 'created_at' })
  createdAt: Date

  constructor(partial: Partial<ChannelEntity>) {
    Object.assign(this, partial)
  }

  @ManyToOne(_type => ServerEntity, server => server.channels, { onDelete: 'CASCADE' })
  @JoinColumn({ name: 'server_id', referencedColumnName: 'id' })
  server: ServerEntity
}
