import { ApiProperty, ApiHideProperty } from '@nestjs/swagger'

import { ServerMemberEntity } from '@modules/server/entities/server-member.entity'
import { ServerEntity } from '@modules/server/entities/server.entity'
import { Expose, Exclude } from 'class-transformer'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, OneToMany } from 'typeorm'

@Entity('user')
export class UserEntity {
  @PrimaryGeneratedColumn('uuid')
  @ApiProperty({ format: 'uuid' })
  id: string

  @Column('int')
  @ApiProperty({ example: 19 })
  tag: number

  @Column()
  @ApiProperty({ example: 'Vassourita' })
  name: string

  @Column()
  @ApiProperty({ format: 'email' })
  email: string

  @Column()
  @Exclude()
  password: string

  @Column({ nullable: true })
  @ApiProperty({ example: 'Insert some lorem ipsum here' })
  status: string

  @Column({ nullable: true })
  @ApiProperty({ example: 'someuserphoto.png' })
  avatar: string

  @Expose()
  @ApiProperty({ example: 'http://localhost:4001/files/someuserphoto.png' })
  get avatarUrl() {
    return `${process.env.PUBLIC_FILES_URL}/${this.avatar}`
  }

  @Expose()
  @ApiProperty({ example: 'Vassourita#0019' })
  get nametag() {
    return `${this.name}#${this.tag.toString().padStart(4, '0')}`
  }

  @CreateDateColumn({ name: 'created_at' })
  createdAt: Date

  constructor(partial: Partial<UserEntity>) {
    Object.assign(this, partial)
  }

  @OneToMany(_type => ServerEntity, s => s.owner)
  @ApiHideProperty()
  ownedServers: ServerEntity[]

  @OneToMany(_type => ServerMemberEntity, s => s.member)
  @ApiHideProperty()
  serverMembers: ServerMemberEntity[]
}
