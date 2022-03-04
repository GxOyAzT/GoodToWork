using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Migrations
{
    public partial class AddUpdatorForStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UpdatorId",
                table: "Status",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Status_UpdatorId",
                table: "Status",
                column: "UpdatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Users_UpdatorId",
                table: "Status",
                column: "UpdatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Status_Users_UpdatorId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Status_UpdatorId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UpdatorId",
                table: "Status");
        }
    }
}
