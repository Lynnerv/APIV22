using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APIV22.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZapatillasPersonalizadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreImagen = table.Column<string>(type: "text", nullable: true),
                    ColorSuperior = table.Column<string>(type: "text", nullable: false),
                    ColorSuela = table.Column<string>(type: "text", nullable: false),
                    ColorCordones = table.Column<string>(type: "text", nullable: false),
                    ColorPlantilla = table.Column<string>(type: "text", nullable: false),
                    Estilo = table.Column<string>(type: "text", nullable: false),
                    Suela = table.Column<string>(type: "text", nullable: false),
                    Texto = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Talla = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZapatillasPersonalizadas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZapatillasPersonalizadas");
        }
    }
}
