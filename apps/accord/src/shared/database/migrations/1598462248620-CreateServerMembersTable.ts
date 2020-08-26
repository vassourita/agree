import { MigrationInterface, QueryRunner } from 'typeorm'

export class CreateServerMembersTable1598462248620 implements MigrationInterface {
  name = 'CreateServerMembersTable1598462248620'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server" DROP CONSTRAINT "FK_552bfb87cd1acce7c512253a774"')
    await queryRunner.query(
      'CREATE TABLE "server_member" ("member_id" uuid NOT NULL, "server_id" uuid NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT now(), CONSTRAINT "PK_e3285349d97b1f532cb3d2663d9" PRIMARY KEY ("member_id", "server_id"))'
    )
    await queryRunner.query(
      'ALTER TABLE "server" ADD CONSTRAINT "FK_552bfb87cd1acce7c512253a774" FOREIGN KEY ("owner_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD CONSTRAINT "FK_313b6fe4d43d67f9fcbdbf59763" FOREIGN KEY ("member_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD CONSTRAINT "FK_9eec2595e538f2892b67cd0bc73" FOREIGN KEY ("server_id") REFERENCES "server"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server_member" DROP CONSTRAINT "FK_9eec2595e538f2892b67cd0bc73"')
    await queryRunner.query('ALTER TABLE "server_member" DROP CONSTRAINT "FK_313b6fe4d43d67f9fcbdbf59763"')
    await queryRunner.query('ALTER TABLE "server" DROP CONSTRAINT "FK_552bfb87cd1acce7c512253a774"')
    await queryRunner.query('DROP TABLE "server_member"')
    await queryRunner.query(
      'ALTER TABLE "server" ADD CONSTRAINT "FK_552bfb87cd1acce7c512253a774" FOREIGN KEY ("owner_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
  }
}
