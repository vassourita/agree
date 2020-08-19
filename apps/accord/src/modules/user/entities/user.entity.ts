import { User } from '@agree/graphql-typedefs'
import {
  Entity,
  PrimaryGeneratedColumn,
  Column,
  CreateDateColumn
} from 'typeorm'

@Entity('user')
export class UserEntity implements User {
  @PrimaryGeneratedColumn('uuid')
  id: string

  @Column()
  name: string

  @Column()
  email: string

  @CreateDateColumn({ name: 'created_at' })
  createdAt: string
}
