using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SampleAPI.Migrations.PersonRepositoryMySqlMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false),
                    last_name = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false),
                    email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false),
                    row_version = table.Column<long>(type: "bigint", nullable: false),
                    sync_version = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Person_created_at",
                table: "Person",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_Person_email",
                table: "Person",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_last_updated_at",
                table: "Person",
                column: "last_updated_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
