import { MigrationInterface, QueryRunner } from 'typeorm'

export class CreateChannelTable1598921858860 implements MigrationInterface {
  name = 'CreateChannelTable1598921858860'

  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "server_member" DROP CONSTRAINT "FK_313b6fe4d43d67f9fcbdbf59763"')
    await queryRunner.query('ALTER TABLE "server_member" DROP CONSTRAINT "FK_9eec2595e538f2892b67cd0bc73"')
    await queryRunner.query('ALTER TABLE "server" DROP CONSTRAINT "FK_552bfb87cd1acce7c512253a774"')
    await queryRunner.query("CREATE TYPE \"channel_type_enum\" AS ENUM('media', 'text')")
    await queryRunner.query(
      'CREATE TABLE "channel" ("id" uuid NOT NULL DEFAULT uuid_generate_v4(), "server_id" uuid NOT NULL, "name" character varying NOT NULL, "type" "channel_type_enum" NOT NULL DEFAULT \'text\', "category" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), CONSTRAINT "PK_590f33ee6ee7d76437acf362e39" PRIMARY KEY ("id"))'
    )
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD CONSTRAINT "FK_313b6fe4d43d67f9fcbdbf59763" FOREIGN KEY ("member_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE NO ACTION'
    )
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD CONSTRAINT "FK_9eec2595e538f2892b67cd0bc73" FOREIGN KEY ("server_id") REFERENCES "server"("id") ON DELETE CASCADE ON UPDATE NO ACTION'
    )
    await queryRunner.query(
      'ALTER TABLE "server" ADD CONSTRAINT "FK_552bfb87cd1acce7c512253a774" FOREIGN KEY ("owner_id") REFERENCES "user"("id") ON DELETE NO ACTION ON UPDATE NO ACTION'
    )
    await queryRunner.query(
      'ALTER TABLE "channel" ADD CONSTRAINT "FK_21f2a6f122b5d2c5f7241672d05" FOREIGN KEY ("server_id") REFERENCES "server"("id") ON DELETE CASCADE ON UPDATE NO ACTION'
    )
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.query('ALTER TABLE "channel" DROP CONSTRAINT "FK_21f2a6f122b5d2c5f7241672d05"')
    await queryRunner.query('ALTER TABLE "server" DROP CONSTRAINT "FK_552bfb87cd1acce7c512253a774"')
    await queryRunner.query('ALTER TABLE "server_member" DROP CONSTRAINT "FK_9eec2595e538f2892b67cd0bc73"')
    await queryRunner.query('ALTER TABLE "server_member" DROP CONSTRAINT "FK_313b6fe4d43d67f9fcbdbf59763"')
    await queryRunner.query('DROP TABLE "channel"')
    await queryRunner.query('DROP TYPE "channel_type_enum"')
    await queryRunner.query(
      'ALTER TABLE "server" ADD CONSTRAINT "FK_552bfb87cd1acce7c512253a774" FOREIGN KEY ("owner_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD CONSTRAINT "FK_9eec2595e538f2892b67cd0bc73" FOREIGN KEY ("server_id") REFERENCES "server"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
    await queryRunner.query(
      'ALTER TABLE "server_member" ADD CONSTRAINT "FK_313b6fe4d43d67f9fcbdbf59763" FOREIGN KEY ("member_id") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE'
    )
  }
}
