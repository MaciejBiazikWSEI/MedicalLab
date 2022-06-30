using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLab.Migrations
{
    public partial class ReduceProjectScope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Tests__TestTypeN__33D4B598",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestTypeName",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PESEL",
                table: "Patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PESEL",
                table: "Patients",
                type: "char(11)",
                unicode: false,
                fixedLength: true,
                maxLength: 11,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestType__737584F739705542", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Parameter__TestT__2E1BDC42",
                        column: x => x.TestTypeName,
                        principalTable: "TestTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    TestCode = table.Column<int>(type: "int", nullable: false),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Results__648CF3D145854DCD", x => new { x.TestCode, x.ParameterId });
                    table.ForeignKey(
                        name: "FK__Results__Paramet__3A81B327",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Results__TestCod__398D8EEE",
                        column: x => x.TestCode,
                        principalTable: "Tests",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTypeName",
                table: "Tests",
                column: "TestTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_TestTypeName",
                table: "Parameters",
                column: "TestTypeName");

            migrationBuilder.CreateIndex(
                name: "UniqueParameter",
                table: "Parameters",
                columns: new[] { "Name", "TestTypeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_ParameterId",
                table: "Results",
                column: "ParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK__Tests__TestTypeN__33D4B598",
                table: "Tests",
                column: "TestTypeName",
                principalTable: "TestTypes",
                principalColumn: "Name");
        }
    }
}
