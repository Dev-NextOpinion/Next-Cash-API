using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Financeiro_Next.Migrations
{
    public partial class ReceitaDespesas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesasFixas_Receitas_ReceitaId",
                table: "DespesasFixas");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesaVariavels_Receitas_ReceitaId",
                table: "DespesaVariavels");

            migrationBuilder.AlterColumn<int>(
                name: "ReceitaId",
                table: "DespesaVariavels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceitaId",
                table: "DespesasFixas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasFixas_Receitas_ReceitaId",
                table: "DespesasFixas",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaVariavels_Receitas_ReceitaId",
                table: "DespesaVariavels",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesasFixas_Receitas_ReceitaId",
                table: "DespesasFixas");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesaVariavels_Receitas_ReceitaId",
                table: "DespesaVariavels");

            migrationBuilder.AlterColumn<int>(
                name: "ReceitaId",
                table: "DespesaVariavels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReceitaId",
                table: "DespesasFixas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasFixas_Receitas_ReceitaId",
                table: "DespesasFixas",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaVariavels_Receitas_ReceitaId",
                table: "DespesaVariavels",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "Id");
        }
    }
}
