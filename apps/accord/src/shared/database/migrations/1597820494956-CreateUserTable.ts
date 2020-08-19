import { MigrationInterface, QueryRunner, Table } from 'typeorm'

export class CreateUserTable1597820494956 implements MigrationInterface {
  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.createTable(
      new Table({
        name: 'user',
        columns: [
          {
            name: 'id',
            type: 'varchar',
            isPrimary: true,
            generationStrategy: 'uuid'
          },
          {
            name: 'name',
            type: 'varchar',
            isNullable: false
          },
          {
            name: 'email',
            type: 'varchar',
            isNullable: false
          },
          {
            name: 'created_at',
            type: 'timestamp with time zone'
          }
        ]
      })
    )
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.dropTable('user')
  }
}
