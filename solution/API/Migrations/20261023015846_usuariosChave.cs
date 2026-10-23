using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class usuariosChave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdAposta = table.Column<int>(type: "INTEGER", nullable: false),
                    NameUser = table.Column<string>(type: "TEXT", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "TEXT", nullable: false),
                    ValorApostado = table.Column<decimal>(type: "TEXT", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Usuarios_PalpitesOdds_IdAposta",
                        column: x => x.IdAposta,
                        principalTable: "PalpitesOdds",
                        principalColumn: "IdAposta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdAposta",
                table: "Usuarios",
                column: "IdAposta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
