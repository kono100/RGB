using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Morador",
                columns: table => new
                {
                    MoradorID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroAP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Morador", x => x.MoradorID);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ReservasID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraRes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuracaoReserva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_MoradorID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ReservasID);
                    table.ForeignKey(
                        name: "FK_Reservas_Morador_fk_MoradorID",
                        column: x => x.fk_MoradorID,
                        principalTable: "Morador",
                        principalColumn: "MoradorID");
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    VeiculoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_MoradorID = table.Column<long>(type: "bigint", nullable: true),
                    VisitanteID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.VeiculoID);
                    table.ForeignKey(
                        name: "FK_Veiculo_Morador_fk_MoradorID",
                        column: x => x.fk_MoradorID,
                        principalTable: "Morador",
                        principalColumn: "MoradorID");
                });

            migrationBuilder.CreateTable(
                name: "Visitante",
                columns: table => new
                {
                    VisitanteID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fk_MoradorID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitante", x => x.VisitanteID);
                    table.ForeignKey(
                        name: "FK_Visitante_Morador_fk_MoradorID",
                        column: x => x.fk_MoradorID,
                        principalTable: "Morador",
                        principalColumn: "MoradorID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_fk_MoradorID",
                table: "Reservas",
                column: "fk_MoradorID");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_fk_MoradorID",
                table: "Veiculo",
                column: "fk_MoradorID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitante_fk_MoradorID",
                table: "Visitante",
                column: "fk_MoradorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Visitante");

            migrationBuilder.DropTable(
                name: "Morador");
        }
    }
}
