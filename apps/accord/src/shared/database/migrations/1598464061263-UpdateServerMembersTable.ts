import { MigrationInterface, QueryRunner } from 'typeorm'

export class UpdateServerMembersTable1598464061263 implements MigrationInterface {
  name = 'UpdateServerMembersTable1598464061263'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" ADD "member_count" integer NOT NULL')
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" DROP COLUMN "member_count"')
  }
}
