import { UserEntity } from '@modules/user/entities/user.entity'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, JoinColumn, ManyToOne } from 'typeorm'

@Entity('server')
export class ServerEntity {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column()
  name: string

  @Column({ name: 'owner_id', type: 'uuid' })
  ownerId: string

  @CreateDateColumn({ name: 'created_at', type: 'timestamp with time zone' })
  createdAt: Date

  constructor(partial: Partial<ServerEntity>) {
    Object.assign(this, partial)
  }

  @ManyToOne(_type => UserEntity, user => user.ownedServers)
  @JoinColumn({ name: 'owner_id', referencedColumnName: 'id' })
  owner: UserEntity
}
