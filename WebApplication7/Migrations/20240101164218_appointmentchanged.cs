using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    public partial class appointmentchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BransId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BransId",
                table: "Appointments",
                column: "BransId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorBranslari_BransId",
                table: "Appointments",
                column: "BransId",
                principalTable: "DoctorBranslari",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorBranslari_BransId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_BransId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "Appointments");
        }
    }
}
