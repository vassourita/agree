import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { Expose, Exclude } from 'class-transformer'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, OneToMany } from 'typeorm'

@Entity('user')
export class UserEntity {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column('int')
  tag: number

  @Column()
  name: string

  @Column()
  email: string

  @Column()
  @Exclude()
  password: string

  @Column({ nullable: true })
  status: string

  @Column({ nullable: true })
  avatar: string

  @Expose()
  get avatarUrl() {
    return `${process.env.PUBLIC_FILES_URL}/${this.avatar}`
  }

  @CreateDateColumn({ name: 'created_at' })
  createdAt: Date

  constructor(partial: Partial<UserEntity>) {
    Object.assign(this, partial)
  }

  @OneToMany(_type => ServerEntity, s => s.owner)
  ownedServers: ServerEntity[]

  @OneToMany(_type => ServerMemberEntity, s => s.member)
  serverMembers: ServerMemberEntity[]
}
