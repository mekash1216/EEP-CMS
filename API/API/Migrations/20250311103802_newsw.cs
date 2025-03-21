using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class newsw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExaminations_PatientId",
                table: "PhysicalExaminations",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExaminations_Patients_PatientId",
                table: "PhysicalExaminations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExaminations_Patients_PatientId",
                table: "PhysicalExaminations");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExaminations_PatientId",
                table: "PhysicalExaminations");
        }
    }
}
