using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class dataDraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "ExpiresAt", "FirstName", "LastName", "Password", "Salt" },
                values: new object[,]
                {
                    { new Guid("a61c0906-b308-4f6e-b860-28f4e3ee8713"), DateTime.UtcNow, new Guid("442a71be-cef4-4dfc-9d3b-1b2bd29556b7"), "faisal@gmail.com", null, "Faisal", "Shahzad", "$2b$10$7qF4Yd4aAJSW0P1AqU0YrOOnP2Et7yCJHfzJwpvO2FTsJsWDdLxF2", "$2b$10$7qF4Yd4aAJSW0P1AqU0YrO" },
                    { new Guid("cfae8354-c7f7-4bd7-af31-43fa991b078e"), DateTime.UtcNow, new Guid("5984c1e3-1edb-49f8-abbc-e695bd6c8ad6"), "marc@gmail.com", null, "Marc", "Josha", "$2b$10$7qF4Yd4aAJSW0P1AqU0YrOOnP2Et7yCJHfzJwpvO2FTsJsWDdLxF2", "$2b$10$7qF4Yd4aAJSW0P1AqU0YrO" },
                    { new Guid("d0413c86-36c6-486b-982d-b13fa76b90b9"), DateTime.UtcNow, new Guid("d8c1f3a8-abb1-4b95-b809-63aa61490d8e"), "tuan@gmail.com", null, "Le", "Tuan", "$2b$10$7qF4Yd4aAJSW0P1AqU0YrOOnP2Et7yCJHfzJwpvO2FTsJsWDdLxF2", "$2b$10$7qF4Yd4aAJSW0P1AqU0YrO" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "CompanyLevel", "CompanyName", "CompanyNo", "CreatedAt", "CreatedBy", "Industry", "NumberOfEmployees", "ParentCompanyId" },
                values: new object[,]
                {
                    { new Guid("050138b6-6c23-4346-88db-87fb74bfcb44"), "Kuala Lumpur", 1, "Accenture", 19, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1989), new Guid("9c967858-7a9d-4d62-bdec-f9aee4c02f1e"), "ITServices", 100, null },
                    { new Guid("39b62d5b-1cb9-476f-9fd3-f67ee3c55436"), "Kuala Lumpur", 0, "Ezyhual", 2, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1915), new Guid("79807b6b-d106-4bde-97ca-bce4feaa2282"), "ITServices", 55, null },
                    { new Guid("3a716e09-c8d9-4e53-8206-2fcc2083927f"), "Kuala Lumpur", 2, "AKS Consultants", 30, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2034), new Guid("694b58df-23f2-4cba-b852-0b7b9a2c717f"), "ITServices", 10, null },
                    { new Guid("3a80ed4e-5724-4799-a5df-cf3a2ab819a4"), "Kuala Lumpur", 1, "KP Info", 20, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1993), new Guid("2ab8e010-8e02-4550-ba0d-66833a8f4920"), "ITServices", 270, null },
                    { new Guid("3dca1e9c-df90-4b41-850e-1231ae301ec4"), "Kuala Lumpur", 1, "Info Tech", 22, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2000), new Guid("d30b0493-117d-4e2c-8765-80574f96658d"), "ITServices", 10, Guid.Parse("050138b6-6c23-4346-88db-87fb74bfcb44") },
                    { new Guid("3e768c9d-cea6-4f4c-b729-1aafd9ae778e"), "Kuala Lumpur", 0, "Faisal n Co Ltd", 9, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1947), new Guid("3d72d29a-7e25-4644-a93e-ad649b8f21f7"), "ITServices", 300, null },
                    { new Guid("3eb8eb70-7c11-4fe9-b3c5-98c79219ace7"), "Kuala Lumpur", 0, "Jabbar Etry", 5, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1930), new Guid("6a2fa70f-8363-4087-9191-d0ea9c7647a9"), "ITServices", 100, null },
                    { new Guid("4a96c358-87e4-4e3f-9cfa-9d22dfe2e557"), "Kuala Lumpur", 0, "KBC Co Ltd", 7, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1939), new Guid("3c7c975b-1162-49cc-b126-43840164939c"), "ITServices", 40, null },
                    { new Guid("531fd700-37c7-47d6-9055-8474f62be903"), "Kuala Lumpur", 0, "Oracle", 1, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1899), new Guid("354b08da-1709-4817-9521-b43fa0947e14"), "ITServices", 10, null },
                    { new Guid("543eccbb-006c-4e16-9b54-ff5c36e6fe9d"), "Kuala Lumpur", 0, "GSG Technologies", 11, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1956), new Guid("cba7ec11-c4c8-4ddd-81a5-e5d3c2df4229"), "ITServices", 770, null },
                    { new Guid("5474783e-3bda-45fd-ac4f-74fc99656259"), "Kuala Lumpur", 2, "Save Mart", 28, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2027), new Guid("8ef6f4c4-8a1c-46c9-9077-85a58c6962ed"), "ITServices", 10, null },
                    { new Guid("591cfc26-5a7f-4927-a658-f211530d6f0d"), "Kuala Lumpur", 0, "Village Groccery", 15, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1973), new Guid("c5e7715f-37f0-443c-9b26-9c0700abd434"), "ITServices", 550, null },
                    { new Guid("5da087f3-9e65-4009-8ab7-e404f927bbbe"), "Kuala Lumpur", 0, "Kings Pharma", 14, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1967), new Guid("0d60efe5-5b3b-4cf7-b0b2-d91ce156be11"), "ITServices", 3000, null },
                    { new Guid("5f074a61-c8e4-4bfe-b02e-4cac4741b51b"), "Kuala Lumpur", 1, "G n G Companies", 16, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1976), new Guid("ede51c1f-6a99-4845-a8b2-a93e0b3ed177"), "ITServices", 350, null },
                    { new Guid("6548679a-d29c-477f-9958-1e982d221515"), "Kuala Lumpur", 1, "House Building Finance", 26, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2017), new Guid("cec76c4a-3bab-4868-ae89-11943fffdb25"), "ITServices", 60, null },
                    { new Guid("6727acc2-d544-48d5-ab81-73a214204202"), "Kuala Lumpur", 0, "May Bank", 13, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1963), new Guid("dc27e925-c694-48fe-9f77-daecc2436298"), "ITServices", 600, null },
                    { new Guid("83a5bcbe-6d6e-4847-92fb-e0f966abd05c"), "Kuala Lumpur", 1, "CIMB Bank", 23, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2006), new Guid("40e68e3f-e41f-443a-a215-1f0559d33130"), "ITServices", 10, null },
                    { new Guid("8af1da6b-2cdb-455f-b37d-496e450e0258"), "Kuala Lumpur", 1, "Chicks and Bigs", 24, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2010), new Guid("fcfc113a-6316-490d-abd3-18d6e8b09fa7"), "ITServices", 50, null },
                    { new Guid("a2503cf6-812c-49d8-95bc-3d95d49af19e"), "Kuala Lumpur", 1, "KFC Fried Chicks", 17, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1980), new Guid("3e677607-956d-4334-9dbb-cc15944becc7"), "ITServices", 10, null },
                    { new Guid("ae32f26c-dd6d-4360-aad4-a76c99bd7234"), "Kuala Lumpur", 1, "Kmpoung KNN", 18, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1984), new Guid("d476f4ce-0249-4ad3-a2b4-f3fd99081b6b"), "ITServices", 10, null },
                    { new Guid("b61c1a5b-7ac5-453d-9fe0-6d2193df89df"), "Kuala Lumpur", 2, "Info Sys", 29, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2030), new Guid("4e24250c-9ca1-4cd4-876d-49de4c344bde"), "ITServices", 10, null },
                    { new Guid("c7311dec-bfa8-4ca2-a897-e5e2e100187d"), "Kuala Lumpur", 0, "Synergy Ltd", 12, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1960), new Guid("c40b842c-291e-4ff5-b7d9-2b98eb050d90"), "ITServices", 1200, null },
                    { new Guid("ce0fe173-d0b7-4aae-a26a-604b67848995"), "Kuala Lumpur", 1, "TCS", 21, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1997), new Guid("9d8fcdf6-3ef0-48d9-a9cc-c77c9d245881"), "ITServices", 10, null },
                    { new Guid("ce3d59ff-4499-4d85-af44-e1b4f78df3f1"), "Kuala Lumpur", 1, "DHL", 25, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2013), new Guid("03931597-82cc-4187-93fe-7e869a12e80b"), "ITServices", 10, null },
                    { new Guid("d9091216-c339-4cfd-805f-75b9eefa8603"), "Kuala Lumpur", 2, "Order Well Co", 27, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(2023), new Guid("e3412ad8-a39d-4fb1-9ad3-0cdd47fcf9ed"), "ITServices", 80, null },
                    { new Guid("dad85e0f-be78-4537-b5a4-e74f747aec46"), "Kuala Lumpur", 0, "Company Three", 3, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1922), new Guid("648ccfcd-309c-4dfc-9d61-bf1f331f7540"), "ITServices", 150, null },
                    { new Guid("dbd62898-0e2c-424d-9d48-42096eaad2e2"), "Kuala Lumpur", 0, "Company Ten", 10, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1950), new Guid("26d4095b-de64-4090-b1a0-c971728ccf08"), "ITServices", 500, null },
                    { new Guid("e10b7c92-75a1-45ea-932e-8804e48a9140"), "Kuala Lumpur", 0, "Company Six", 6, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1933), new Guid("d27ccd89-7763-4798-a351-b95911831b5b"), "ITServices", 10, null },
                    { new Guid("edbc63e3-fa4b-47d0-a1d6-5e5d5f4f1913"), "Kuala Lumpur", 0, "Company Four", 4, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1926), new Guid("062ed847-3047-4266-a2fb-999aa09d9e9b"), "ITServices", 30, null },
                    { new Guid("ef4cd96e-90fd-4e59-8fcd-f7421db8606d"), "Kuala Lumpur", 0, "Company Eight", 8, new DateTime(2023, 11, 1, 10, 38, 52, 934, DateTimeKind.Local).AddTicks(1943), new Guid("27ebaff4-c17c-4d1b-8d10-885b2d4a2f32"), "ITServices", 10, null }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("050138b6-6c23-4346-88db-87fb74bfcb44"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("39b62d5b-1cb9-476f-9fd3-f67ee3c55436"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("3a716e09-c8d9-4e53-8206-2fcc2083927f"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("3a80ed4e-5724-4799-a5df-cf3a2ab819a4"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("3dca1e9c-df90-4b41-850e-1231ae301ec4"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("3e768c9d-cea6-4f4c-b729-1aafd9ae778e"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("3eb8eb70-7c11-4fe9-b3c5-98c79219ace7"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("4a96c358-87e4-4e3f-9cfa-9d22dfe2e557"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("531fd700-37c7-47d6-9055-8474f62be903"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("543eccbb-006c-4e16-9b54-ff5c36e6fe9d"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5474783e-3bda-45fd-ac4f-74fc99656259"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("591cfc26-5a7f-4927-a658-f211530d6f0d"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5da087f3-9e65-4009-8ab7-e404f927bbbe"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f074a61-c8e4-4bfe-b02e-4cac4741b51b"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6548679a-d29c-477f-9958-1e982d221515"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6727acc2-d544-48d5-ab81-73a214204202"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("83a5bcbe-6d6e-4847-92fb-e0f966abd05c"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("8af1da6b-2cdb-455f-b37d-496e450e0258"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("a2503cf6-812c-49d8-95bc-3d95d49af19e"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("ae32f26c-dd6d-4360-aad4-a76c99bd7234"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("b61c1a5b-7ac5-453d-9fe0-6d2193df89df"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("c7311dec-bfa8-4ca2-a897-e5e2e100187d"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("ce0fe173-d0b7-4aae-a26a-604b67848995"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("ce3d59ff-4499-4d85-af44-e1b4f78df3f1"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("d9091216-c339-4cfd-805f-75b9eefa8603"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("dad85e0f-be78-4537-b5a4-e74f747aec46"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("dbd62898-0e2c-424d-9d48-42096eaad2e2"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e10b7c92-75a1-45ea-932e-8804e48a9140"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("edbc63e3-fa4b-47d0-a1d6-5e5d5f4f1913"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("ef4cd96e-90fd-4e59-8fcd-f7421db8606d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a61c0906-b308-4f6e-b860-28f4e3ee8713"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cfae8354-c7f7-4bd7-af31-43fa991b078e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0413c86-36c6-486b-982d-b13fa76b90b9"));

        }
    }
}
