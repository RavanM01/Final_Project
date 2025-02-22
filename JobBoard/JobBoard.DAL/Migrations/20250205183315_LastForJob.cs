using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LastForJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserJob_AspNetUsers_AppUsersId",
                table: "AppUserJob");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserJob_Jobs_JobsId",
                table: "AppUserJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserJob",
                table: "AppUserJob");

            migrationBuilder.RenameColumn(
                name: "JobsId",
                table: "AppUserJob",
                newName: "JobId");

            migrationBuilder.RenameColumn(
                name: "AppUsersId",
                table: "AppUserJob",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserJob_JobsId",
                table: "AppUserJob",
                newName: "IX_AppUserJob_JobId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppUserJob",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "AppUserJob",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserJob",
                table: "AppUserJob",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserJob_CreatedByUserId",
                table: "AppUserJob",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserJob_UserId",
                table: "AppUserJob",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserJob_AspNetUsers_CreatedByUserId",
                table: "AppUserJob",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserJob_AspNetUsers_UserId",
                table: "AppUserJob",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserJob_Jobs_JobId",
                table: "AppUserJob",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserJob_AspNetUsers_CreatedByUserId",
                table: "AppUserJob");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserJob_AspNetUsers_UserId",
                table: "AppUserJob");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserJob_Jobs_JobId",
                table: "AppUserJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserJob",
                table: "AppUserJob");

            migrationBuilder.DropIndex(
                name: "IX_AppUserJob_CreatedByUserId",
                table: "AppUserJob");

            migrationBuilder.DropIndex(
                name: "IX_AppUserJob_UserId",
                table: "AppUserJob");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppUserJob");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AppUserJob");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppUserJob",
                newName: "AppUsersId");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "AppUserJob",
                newName: "JobsId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserJob_JobId",
                table: "AppUserJob",
                newName: "IX_AppUserJob_JobsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserJob",
                table: "AppUserJob",
                columns: new[] { "AppUsersId", "JobsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserJob_AspNetUsers_AppUsersId",
                table: "AppUserJob",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserJob_Jobs_JobsId",
                table: "AppUserJob",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
