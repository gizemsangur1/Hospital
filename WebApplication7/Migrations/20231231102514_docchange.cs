using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    public partial class docchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorBranslari_DoktorBransId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DoktorBransId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoktorBransId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorBransId",
                table: "Doctors",
                column: "DoctorBransId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorBranslari_DoctorBransId",
                table: "Doctors",
                column: "DoctorBransId",
                principalTable: "DoctorBranslari",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorBranslari_DoctorBransId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DoctorBransId",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "DoktorBransId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoktorBransId",
                table: "Doctors",
                column: "DoktorBransId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorBranslari_DoktorBransId",
                table: "Doctors",
                column: "DoktorBransId",
                principalTable: "DoctorBranslari",
                principalColumn: "Id");
        }
    }
}
