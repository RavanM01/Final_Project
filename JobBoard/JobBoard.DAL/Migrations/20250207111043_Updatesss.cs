using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Updatesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "AppUserJob",
                newName: "UserJobs");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserJob_UserId",
                table: "UserJobs",
                newName: "IX_UserJobs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserJob_JobId",
                table: "UserJobs",
                newName: "IX_UserJobs_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserJob_CreatedByUserId",
                table: "UserJobs",
                newName: "IX_UserJobs_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserJobs",
                table: "UserJobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobs_AspNetUsers_CreatedByUserId",
                table: "UserJobs",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobs_AspNetUsers_UserId",
                table: "UserJobs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobs_Jobs_JobId",
                table: "UserJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJobs_AspNetUsers_CreatedByUserId",
                table: "UserJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobs_AspNetUsers_UserId",
                table: "UserJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobs_Jobs_JobId",
                table: "UserJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserJobs",
                table: "UserJobs");

            migrationBuilder.RenameTable(
                name: "UserJobs",
                newName: "AppUserJob");

            migrationBuilder.RenameIndex(
                name: "IX_UserJobs_UserId",
                table: "AppUserJob",
                newName: "IX_AppUserJob_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserJobs_JobId",
                table: "AppUserJob",
                newName: "IX_AppUserJob_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_UserJobs_CreatedByUserId",
                table: "AppUserJob",
                newName: "IX_AppUserJob_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserJob",
                table: "AppUserJob",
                column: "Id");

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
    }
}
