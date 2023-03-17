using Microsoft.EntityFrameworkCore.Migrations;

namespace Talabat.DAL.Data.Migrations
{
    public partial class RenamePictueUrlColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictueUrl",
                table: "Products",
                newName: "PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Products",
                newName: "PictueUrl");
        }
    }
}
