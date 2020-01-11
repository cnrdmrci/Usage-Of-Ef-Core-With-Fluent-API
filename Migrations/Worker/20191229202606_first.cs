using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreExamples.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    WorkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.WorkerId);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    ContactInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.ContactInfoId);
                    table.ForeignKey(
                        name: "FK_ContactInfo_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_WorkerId",
                table: "ContactInfo",
                column: "WorkerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "Worker");
        }
    }
}
