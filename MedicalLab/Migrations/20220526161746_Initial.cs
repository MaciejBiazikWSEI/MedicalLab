using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLab.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    PESEL = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patients__A25C5AA63D960BA2", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Testers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testers", x => x.Id);
                });

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
                name: "Samples",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientCode = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Samples__A25C5AA6AFC9BB1D", x => x.Code);
                    table.ForeignKey(
                        name: "FK__Samples__Patient__30F848ED",
                        column: x => x.PatientCode,
                        principalTable: "Patients",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TestTypeName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
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
                name: "Tests",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SampleCode = table.Column<int>(type: "int", nullable: false),
                    TesterId = table.Column<int>(type: "int", nullable: false),
                    DateFinished = table.Column<DateTime>(type: "date", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tests__A25C5AA605BCDABF", x => x.Code);
                    table.ForeignKey(
                        name: "FK__Tests__SampleCod__34C8D9D1",
                        column: x => x.SampleCode,
                        principalTable: "Samples",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tests__TesterId__35BCFE0A",
                        column: x => x.TesterId,
                        principalTable: "Testers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Tests__TestTypeN__33D4B598",
                        column: x => x.TestTypeName,
                        principalTable: "TestTypes",
                        principalColumn: "Name");
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

            migrationBuilder.CreateIndex(
                name: "IX_Samples_PatientCode",
                table: "Samples",
                column: "PatientCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SampleCode",
                table: "Tests",
                column: "SampleCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TesterId",
                table: "Tests",
                column: "TesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTypeName",
                table: "Tests",
                column: "TestTypeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "Testers");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
