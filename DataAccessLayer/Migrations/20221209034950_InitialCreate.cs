using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NumberOfPictures = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlbumPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    AdditionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumPhotos_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlbumPhotos_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    RatingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosRating_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhotosRating_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoTags_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhotoTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Animals" },
                    { 2, "Cities" },
                    { 3, "Japan" },
                    { 4, "Rivers" },
                    { 5, "Mountains" },
                    { 6, "Cars" },
                    { 7, "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "EmailAddress", "FirstName", "LastName", "Password", "PasswordSalt", "RegistrationDate", "Role", "UserName", "UserStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael_jordan@gmail.com", "Michael", "Jordan", "qyzQ2nwWrhP2B2GW5zyA474VPKRBI5SiBoQTQL9WIRQ=", "04GspiCZJ5P7/iN3bvcKwqTRpQ1Cy74ven+2k6S4Rb1g45G6TSs7PdntzXipdwC3U9MIXRiW34fLFWBeGHigCiy6JVkoScXmrrZyU/eqv6yrhYNjk97s5PxlYUBQNXVtq5x8gTzkjvqJUz4k12Lh9ZDnxW8fGjargWNFimUrOxs=", new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "jordan100", 0 },
                    { 2, new DateTime(1990, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "john_peterson@gmail.com", "John", "Peterson", "pJjgiLe125FtR/0nLpPDrLp4yQ8VHGvcDsQz2S+Zo6Q=", "nBUmfMoNvbXWJz9IBExvRsf3aXqBNQwjvengMFtottifM5BXO9Dd+ptLFyuVup0v2ptnkkT07E2tFk3jKlNqlKQvO0/tifU0RE8BZOctlT+KUQVRm4h4DwOCisZXA5M2OyKav0QRlDcVYf2qZSV9QzJEgE0YPrsy8UAaXOQshT8=", new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "johny1", 0 },
                    { 3, new DateTime(1963, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "barack_obama@gmail.com", "Barack", "Obama", "hUDm7nRCZxxDY+q1mVigaSoXWTqMyaPUc7ApQcHe2LQ=", "azVVLWQdzIwg4CzTUJfUeLRunjLSCc1HiSMAgHjjBapTDRvAHkX55r3M2wk2c4z6+XRJxPq07vxFucpDZ8CKTRyUCLpgYmY7or3w/Vb0oqezGL1loBiQnLQooWO8ZppoBf7zaSPut48sFob7AYKA0w4Gm+ZQ7fwxMYUQouQrEo4=", new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "barack_usa", 0 }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "Description", "PhotoUrl", "Title", "TotalRating", "UploadDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/animals1.jpg", "Deer", 5.0, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/car1.jpg", "Car in woods", 3.0, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/city4.jpg", "Japanese city", 4.0, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/ukraine1.jpg", "Kyiv", 5.0, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/ukraine3.jpg", "Field", 5.0, new DateTime(2018, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/cars1.jpg", "Mercedes", 2.5, new DateTime(2015, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/car2.jpg", "Porsche", 2.5, new DateTime(2022, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 8, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/mountain1.jpg", "Japan mountain", 3.0, new DateTime(2019, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "/assets/images/db/nature1.jpg", "Hills", 4.7000000000000002, new DateTime(2018, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPhotos_AlbumId",
                table: "AlbumPhotos",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPhotos_PhotoId",
                table: "AlbumPhotos",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosRating_PhotoId",
                table: "PhotosRating",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosRating_UserId",
                table: "PhotosRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoTags_PhotoId",
                table: "PhotoTags",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoTags_TagId",
                table: "PhotoTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PhotoId",
                table: "Reports",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumPhotos");

            migrationBuilder.DropTable(
                name: "PhotosRating");

            migrationBuilder.DropTable(
                name: "PhotoTags");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
