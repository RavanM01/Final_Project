using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "AspNetUsers",
                newName: "LogoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "AspNetUsers",
                newName: "Logo");
        }
    }
}
