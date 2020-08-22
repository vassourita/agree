import { User } from '@agree/graphql-typedefs'

import { Expose } from 'class-transformer'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn } from 'typeorm'

@Entity('user')
export class UserEntity implements Omit<User, 'createdAt'> {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column('int')
  tag: number

  @Column()
  name: string

  @Column()
  email: string

  @Column()
  password: string

  @Column({ nullable: true })
  status: string

  @Column({ nullable: true })
  avatar: string

  @Expose()
  get avatarUrl() {
    return `${process.env.PUBLIC_FILES_URL}/${this.avatar}`
  }

  @CreateDateColumn({ name: 'created_at', type: 'timestamp with time zone' })
  createdAt: Date

  constructor(data: Partial<UserEntity>) {
    Object.assign(this, data)
  }
}
