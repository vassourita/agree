import { MigrationInterface, QueryRunner } from 'typeorm'

export class CreateServerTable1598460740090 implements MigrationInterface {
  name = 'CreateServerTable1598460740090'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query(
      'CREATE TABLE "server" ("id" uuid NOT NULL DEFAULT uuid_generate_v4(), "name" character varying NOT NULL, "owner_id" uuid NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT now(), CONSTRAINT "PK_f8b8af38bdc23b447c0a57c7937" PRIMARY KEY ("id"))'
    )
    await queryRunner.query(
      'ALTER TABLE "server" ADD CONSTRAINT "FK_552bfb87cd1acce7c512253a774" FOREIGN KEY ("owner_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" DROP CONSTRAINT "FK_552bfb87cd1acce7c512253a774"')
    await queryRunner.query('DROP TABLE "server"')
  }
}
