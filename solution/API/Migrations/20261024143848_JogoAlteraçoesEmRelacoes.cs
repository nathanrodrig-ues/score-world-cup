using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class JogoAlteraçoesEmRelacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PalpitesOdds_IdAposta",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdAposta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdAposta",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Seleçoes",
                newName: "IdSelecao");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PalpitesOdds",
                newName: "IdUser");

            migrationBuilder.AddColumn<int>(
                name: "IdJogo",
                table: "PalpitesOdds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdSelecao1 = table.Column<int>(type: "INTEGER", nullable: false),
                    IdSelecao2 = table.Column<int>(type: "INTEGER", nullable: false),
                    HorarioJogo = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogos_Seleçoes_IdSelecao1",
                        column: x => x.IdSelecao1,
                        principalTable: "Seleçoes",
                        principalColumn: "IdSelecao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogos_Seleçoes_IdSelecao2",
                        column: x => x.IdSelecao2,
                        principalTable: "Seleçoes",
                        principalColumn: "IdSelecao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PalpitesOdds_IdJogo",
                table: "PalpitesOdds",
                column: "IdJogo");

            migrationBuilder.CreateIndex(
                name: "IX_PalpitesOdds_IdUser",
                table: "PalpitesOdds",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_IdSelecao1",
                table: "Jogos",
                column: "IdSelecao1");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_IdSelecao2",
                table: "Jogos",
                column: "IdSelecao2");

            migrationBuilder.AddForeignKey(
                name: "FK_PalpitesOdds_Jogos_IdJogo",
                table: "PalpitesOdds",
                column: "IdJogo",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PalpitesOdds_Usuarios_IdUser",
                table: "PalpitesOdds",
                column: "IdUser",
                principalTable: "Usuarios",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PalpitesOdds_Jogos_IdJogo",
                table: "PalpitesOdds");

            migrationBuilder.DropForeignKey(
                name: "FK_PalpitesOdds_Usuarios_IdUser",
                table: "PalpitesOdds");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_PalpitesOdds_IdJogo",
                table: "PalpitesOdds");

            migrationBuilder.DropIndex(
                name: "IX_PalpitesOdds_IdUser",
                table: "PalpitesOdds");

            migrationBuilder.DropColumn(
                name: "IdJogo",
                table: "PalpitesOdds");

            migrationBuilder.RenameColumn(
                name: "IdSelecao",
                table: "Seleçoes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "PalpitesOdds",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "IdAposta",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdAposta",
                table: "Usuarios",
                column: "IdAposta");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_PalpitesOdds_IdAposta",
                table: "Usuarios",
                column: "IdAposta",
                principalTable: "PalpitesOdds",
                principalColumn: "IdAposta",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
