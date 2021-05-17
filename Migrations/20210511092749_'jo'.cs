using Microsoft.EntityFrameworkCore.Migrations;

namespace ESCOM_FLEET_SYSTEM.Migrations
{
    public partial class jo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_Department_DepartmentNameDepartmentId",
                table: "Station");

            migrationBuilder.DropIndex(
                name: "IX_Station_DepartmentNameDepartmentId",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "DepartmentNameDepartmentId",
                table: "Station");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Station",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Station_DepartmentId",
                table: "Station",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Department_DepartmentId",
                table: "Station",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_Department_DepartmentId",
                table: "Station");

            migrationBuilder.DropIndex(
                name: "IX_Station_DepartmentId",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Station");

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Station",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentNameDepartmentId",
                table: "Station",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Station_DepartmentNameDepartmentId",
                table: "Station",
                column: "DepartmentNameDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Department_DepartmentNameDepartmentId",
                table: "Station",
                column: "DepartmentNameDepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
