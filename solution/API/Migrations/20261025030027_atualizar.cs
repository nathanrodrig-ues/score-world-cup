using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class atualizar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PalpitesOdds_IdAposta",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "PalpitesOdds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdAposta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdAposta",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "Usuarios",
                newName: "CpfUsuario");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Seleçoes",
                newName: "IdSelecao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "CpfUsuario");

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    IdJogo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CpfUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    IdSelecao1 = table.Column<int>(type: "INTEGER", nullable: false),
                    IdSelecao2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.IdJogo);
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
                    table.ForeignKey(
                        name: "FK_Jogos_Usuarios_CpfUsuario",
                        column: x => x.CpfUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "CpfUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Palpites",
                columns: table => new
                {
                    IdAposta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdJogo = table.Column<int>(type: "INTEGER", nullable: false),
                    ResultadoSelecao1 = table.Column<int>(type: "INTEGER", nullable: false),
                    ResultadoSelecao2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Odd = table.Column<decimal>(type: "TEXT", nullable: false),
                    OldAposta = table.Column<decimal>(type: "TEXT", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palpites", x => x.IdAposta);
                    table.ForeignKey(
                        name: "FK_Palpites_Jogos_IdJogo",
                        column: x => x.IdJogo,
                        principalTable: "Jogos",
                        principalColumn: "IdJogo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_CpfUsuario",
                table: "Jogos",
                column: "CpfUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_IdSelecao1",
                table: "Jogos",
                column: "IdSelecao1");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_IdSelecao2",
                table: "Jogos",
                column: "IdSelecao2");

            migrationBuilder.CreateIndex(
                name: "IX_Palpites_IdJogo",
                table: "Palpites",
                column: "IdJogo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palpites");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "CpfUsuario",
                table: "Usuarios",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "IdSelecao",
                table: "Seleçoes",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "IdAposta",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "IdUser");

            migrationBuilder.CreateTable(
                name: "PalpitesOdds",
                columns: table => new
                {
                    IdAposta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Derrota = table.Column<bool>(type: "INTEGER", nullable: false),
                    Empate = table.Column<bool>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    OldAposta = table.Column<decimal>(type: "TEXT", nullable: false),
                    Vitoria = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalpitesOdds", x => x.IdAposta);
                });

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
