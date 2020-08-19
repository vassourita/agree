import { User } from '@agree/graphql-typedefs'

import { Expose, Exclude } from 'class-transformer'
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
  @Exclude()
  password: string

  @Column()
  status: string

  @Column()
  avatar: string

  @Expose()
  get avatarUrl() {
    return ''
  }

  @CreateDateColumn({ name: 'created_at', type: 'timestamp with time zone' })
  createdAt: Date

  constructor(data: Partial<UserEntity>) {
    Object.assign(this, data)
  }
}
