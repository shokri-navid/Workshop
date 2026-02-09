using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class SeedPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_PersonId",
                table: "Skills");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "BirthDay", "Familly", "Name" },
                values: new object[,]
                {
                    { new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb778e"), "Shahmirzad", new DateTime(1988, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shokri", "Navid" },
                    { new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb779e"), "Semnan", new DateTime(1988, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salavati", "Saeed" },
                    { new Guid("c5312d91-11c5-4c4e-99cf-6e9f5fcb778e"), "Shahmirzad", new DateTime(1986, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shokri", "Ali" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "PersonId", "Title" },
                values: new object[,]
                {
                    { new Guid("c5312d91-00c5-3c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-11c5-4c4e-99cf-6e9f5fcb778e"), "Coaching" },
                    { new Guid("c5312d91-00c5-4c4e-10cf-6e9f5fcb778e"), new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb779e"), "Accounting" },
                    { new Guid("c5312d91-10c5-4c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb778e"), "SWE" },
                    { new Guid("c5312d91-12c5-4c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-11c5-4c4e-99cf-6e9f5fcb778e"), "Construction" },
                    { new Guid("c5312d91-15c5-4c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-11c5-4c4e-99cf-6e9f5fcb778e"), "Communication" },
                    { new Guid("c5312d91-18c5-4c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb778e"), "Welding" },
                    { new Guid("c5312d91-20c5-4c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb778e"), "Coaching" },
                    { new Guid("c5312d91-45c5-4c4e-99cf-6e9f5fcb778e"), new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb779e"), "Mechanics" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_PersonId",
                table: "Skills",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_PersonId",
                table: "Skills");

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-00c5-3c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-00c5-4c4e-10cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-10c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-12c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-15c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-18c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-20c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-45c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-00c5-4c4e-99cf-6e9f5fcb779e"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("c5312d91-11c5-4c4e-99cf-6e9f5fcb778e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_PersonId",
                table: "Skills",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
