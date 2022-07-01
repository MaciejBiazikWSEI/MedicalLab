using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLab.Migrations
{
    public partial class FixDeleteSample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Samples__Patient__30F848ED",
                table: "Samples");

            migrationBuilder.AddForeignKey(
                name: "FK__Samples__Patient__30F848ED",
                table: "Samples",
                column: "PatientCode",
                principalTable: "Patients",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Samples__Patient__30F848ED",
                table: "Samples");

            migrationBuilder.AddForeignKey(
                name: "FK__Samples__Patient__30F848ED",
                table: "Samples",
                column: "PatientCode",
                principalTable: "Patients",
                principalColumn: "Code");
        }
    }
}
