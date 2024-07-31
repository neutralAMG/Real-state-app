using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infraestructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeOnDeleteToPropertyOnPropertyPerks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPerks_Properties_PropertyId",
                table: "PropertyPerks");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPerks_Properties_PropertyId",
                table: "PropertyPerks",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPerks_Properties_PropertyId",
                table: "PropertyPerks");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPerks_Properties_PropertyId",
                table: "PropertyPerks",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
