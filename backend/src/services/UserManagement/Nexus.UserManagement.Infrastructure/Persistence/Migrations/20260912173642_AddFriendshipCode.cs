using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.UserManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendshipCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "friendship_code",
                table: "users",
                type: "text",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS pgcrypto;");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION generate_friendship_code()
                RETURNS TEXT AS $$
                DECLARE
                    alphabet TEXT := '23456789ABCDEFGHJKMNPQRSTVWXYZ';
                    result   TEXT := '';
                    bytes    BYTEA;
                    i        INT;
                    idx      INT;
                BEGIN
                    bytes := gen_random_bytes(15);
                    FOR i IN 0..14 LOOP
                        IF i = 5 OR i = 10 THEN
                            result := result || '-';
                        END IF;
                        
                        idx := (get_byte(bytes, i) % 32) + 1;
                        result := result || substr(alphabet, idx, 1);
                    END LOOP;
                    RETURN result;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                DO $$
                DECLARE
                    collision_exists BOOLEAN := true;
                    attempt INT := 0;
                BEGIN
                    WHILE collision_exists AND attempt < 5 LOOP
                        UPDATE users
                        SET friendship_code = generate_friendship_code()
                        WHERE friendship_code IS NULL;

                        collision_exists := EXISTS (
                            SELECT friendship_code, COUNT(*)
                            FROM users
                            WHERE friendship_code IS NOT NULL
                            GROUP BY friendship_code
                            HAVING COUNT(*) > 1
                        );

                        IF collision_exists THEN
                            UPDATE users
                            SET friendship_code = NULL
                            WHERE friendship_code IN (
                                SELECT friendship_code
                                FROM users
                                GROUP BY friendship_code
                                HAVING COUNT(*) > 1
                            );
                        END IF;

                        attempt := attempt + 1;
                    END LOOP;
                END $$;
            ");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS generate_friendship_code();");

            migrationBuilder.AlterColumn<string>(
                name: "friendship_code",
                table: "users",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "case_insensitive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FriendshipCode",
                table: "users",
                column: "friendship_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_FriendshipCode",
                table: "users");

            migrationBuilder.DropColumn(
                name: "friendship_code",
                table: "users");
        }
    }
}