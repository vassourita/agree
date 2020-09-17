import { MigrationInterface, QueryRunner } from 'typeorm'

export class UpdateServerTable1600300725025 implements MigrationInterface {
  name = 'UpdateServerTable1600300725025'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" ADD "avatar" character varying')
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" DROP COLUMN "avatar"')
  }
}
