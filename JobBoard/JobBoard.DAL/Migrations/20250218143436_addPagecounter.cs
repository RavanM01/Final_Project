using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addPagecounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Jobs");
        }
    }
}
