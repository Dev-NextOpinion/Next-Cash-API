using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Financeiro_Next.Migrations.Usuarios
{
    public partial class UpdateUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "AspNetUsers");
        }
    }
}
