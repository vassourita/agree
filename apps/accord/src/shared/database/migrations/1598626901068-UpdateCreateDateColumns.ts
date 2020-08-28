import { MigrationInterface, QueryRunner } from 'typeorm'

export class UpdateCreateDateColumns1598626901068 implements MigrationInterface {
  name = 'UpdateCreateDateColumns1598626901068'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" DROP COLUMN "created_at"')
    await queryRunner.query('ALTER TABLE "server" ADD "created_at" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT now()')
    await queryRunner.query('ALTER TABLE "user" DROP COLUMN "created_at"')
    await queryRunner.query('ALTER TABLE "user" ADD "created_at" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT now()')
    await queryRunner.query('ALTER TABLE "server_member" DROP COLUMN "created_at"')
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD "created_at" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT now()'
    )
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server_member" DROP COLUMN "created_at"')
    await queryRunner.query('ALTER TABLE "server_member" ADD "created_at" TIMESTAMP NOT NULL DEFAULT now()')
    await queryRunner.query('ALTER TABLE "user" DROP COLUMN "created_at"')
    await queryRunner.query('ALTER TABLE "user" ADD "created_at" TIMESTAMP NOT NULL DEFAULT now()')
    await queryRunner.query('ALTER TABLE "server" DROP COLUMN "created_at"')
    await queryRunner.query('ALTER TABLE "server" ADD "created_at" TIMESTAMP NOT NULL DEFAULT now()')
  }
}
