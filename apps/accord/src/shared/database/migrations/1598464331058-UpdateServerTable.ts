import { MigrationInterface, QueryRunner } from 'typeorm'

export class UpdateServerTable1598464331058 implements MigrationInterface {
  name = 'UpdateServerTable1598464331058'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" ALTER COLUMN "member_count" SET DEFAULT 0')
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" ALTER COLUMN "member_count" DROP DEFAULT')
  }
}
