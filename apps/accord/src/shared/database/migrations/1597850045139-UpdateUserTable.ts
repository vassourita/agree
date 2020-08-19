import { MigrationInterface, QueryRunner } from 'typeorm'

export class UpdateUserTable1597850045139 implements MigrationInterface {
  name = 'UpdateUserTable1597850045139'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "user" ADD "tag" integer NOT NULL')
    await queryRunner.query('ALTER TABLE "user" ADD "password" character varying NOT NULL')
    await queryRunner.query('ALTER TABLE "user" ADD "status" character varying NOT NULL')
    await queryRunner.query('ALTER TABLE "user" ADD "avatar" character varying NOT NULL')
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "user" DROP COLUMN "avatar"')
    await queryRunner.query('ALTER TABLE "user" DROP COLUMN "status"')
    await queryRunner.query('ALTER TABLE "user" DROP COLUMN "password"')
    await queryRunner.query('ALTER TABLE "user" DROP COLUMN "tag"')
  }
}
