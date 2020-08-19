import { User } from '@agree/graphql-typedefs'
import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn } from 'typeorm'

@Entity('user')
export class UserEntity implements Omit<User, 'createdAt'> {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column()
  name: string

  @Column()
  email: string

  @CreateDateColumn({ name: 'created_at', type: 'timestamp with time zone' })
  createdAt: Date

  constructor(data: Partial<UserEntity>) {
    Object.assign(this, data)
  }
}
