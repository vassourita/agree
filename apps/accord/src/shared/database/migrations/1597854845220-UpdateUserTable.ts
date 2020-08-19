import {MigrationInterface, QueryRunner} from "typeorm";

export class UpdateUserTable1597854845220 implements MigrationInterface {
    name = 'UpdateUserTable1597854845220'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "user" ALTER COLUMN "status" DROP NOT NULL`);
        await queryRunner.query(`ALTER TABLE "user" ALTER COLUMN "avatar" DROP NOT NULL`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "user" ALTER COLUMN "avatar" SET NOT NULL`);
        await queryRunner.query(`ALTER TABLE "user" ALTER COLUMN "status" SET NOT NULL`);
    }

}
