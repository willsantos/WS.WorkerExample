using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WS.WorkerExample.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposControleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AtualizadoEm",
                table: "Funcionarios",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CriadoEm",
                table: "Funcionarios",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExcluidoEm",
                table: "Funcionarios",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AtualizadoEm",
                table: "Empresas",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CriadoEm",
                table: "Empresas",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExcluidoEm",
                table: "Empresas",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "ExcluidoEm",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "ExcluidoEm",
                table: "Empresas");
        }
    }
}
