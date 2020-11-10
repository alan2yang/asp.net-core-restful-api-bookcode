using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeDemo.Migrations
{
    public partial class addseddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), new DateTimeOffset(new DateTime(1960, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Beijing", "author1@xxx.com", "Author 1" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), new DateTimeOffset(new DateTime(1976, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Hubei", "author2@xxx.com", "Author 2" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("8406b13e-a793-4b12-84cb-7fe2a694b9aa"), new DateTimeOffset(new DateTime(1973, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Hubei", "author3@xxx.com", "Author 3" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("74556abd-1a6c-4d20-a8a7-271dd4393b2e"), new DateTimeOffset(new DateTime(1978, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Shandong", "author4@xxx.com", "Author 4" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("1029db57-c15c-4c0c-80a0-c811b7995cb4"), new DateTimeOffset(new DateTime(1973, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Beijing", "author5@xxx.com", "Author 5" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("0f978cf6-df6d-47a9-8ef2-d2723cc29cc8"), new DateTimeOffset(new DateTime(1981, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Beijing", "author6@xxx.com", "Author 6" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("10ee3976-d672-4411-ae1c-3267baa940eb"), new DateTimeOffset(new DateTime(1954, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Shandong", "author7@xxx.com", "Author 7" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("2633a79c-9f4a-48d5-ae5a-70945fb8583c"), new DateTimeOffset(new DateTime(1981, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Shandong", "author8@xxx.com", "Author 8" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("7d8ebda9-2634-4c0f-9469-0695d6132153"), new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), "Description of Book 1", 281, "Book 1" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("1ed47697-aa7d-48c2-aa39-305d0e13b3aa"), new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), "Description of Book 2", 370, "Book 2" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("5f82c852-375d-4926-a3b7-84b63fc1bfae"), new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), "Description of Book 3", 229, "Book 3" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("418a5b20-460b-4604-be17-2b0809e19acd"), new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), "Description of Book 4", 440, "Book 4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0f978cf6-df6d-47a9-8ef2-d2723cc29cc8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1029db57-c15c-4c0c-80a0-c811b7995cb4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("10ee3976-d672-4411-ae1c-3267baa940eb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2633a79c-9f4a-48d5-ae5a-70945fb8583c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("74556abd-1a6c-4d20-a8a7-271dd4393b2e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8406b13e-a793-4b12-84cb-7fe2a694b9aa"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("1ed47697-aa7d-48c2-aa39-305d0e13b3aa"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("418a5b20-460b-4604-be17-2b0809e19acd"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5f82c852-375d-4926-a3b7-84b63fc1bfae"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7d8ebda9-2634-4c0f-9469-0695d6132153"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"));
        }
    }
}
