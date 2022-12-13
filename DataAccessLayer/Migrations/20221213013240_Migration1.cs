using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael_jordan@gmail.com", "Michael", "Jordan", "p6RQ3yI1RcKmNc7yPWgBV7UObduiFzi4Fx5Lbj4jnOw=", "K0+QtIwuQbFLa8PYNU2pvQAXpXev+ED19dFefPsR2jQgNkQR1UX3ZVeGBayzVM3wkE3fRxXl5YhS/+UmledlpTBpCjIh1AhIKisoZBEP+bNUrKRFzCo4aDCX6NcKXYF1JJtFN05WN6U76DqJ/nTQAtiC2ID5T3Mz5ksKo6HbBrk=", new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "jordan100", 0 },
                    { 2, new DateTime(1990, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "john_peterson@gmail.com", "John", "Peterson", "g6tkuHb1XNUHr+d58XPpB2IV6BWRXDqNV0DeozBMCgs=", "5Lw7vE1pUmoxWdFanx0AutaWwbAGLCNUFJRBYz5rJN8gijPglGiCeKMQojTkFtg3bKbpLsVoh3tUU16Vp2S4P6K00oPViuK7v74o3WZHXmcQ7+IV5bYFbGibvpIjupoPteAgoPPY6OdwozZE9WkaJ/gn13m7BXCAZ5QZ4mLFHKk=", new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "johny1", 0 },
                    { 3, new DateTime(1963, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "barack_obama@gmail.com", "Barack", "Obama", "f0higKoWg3+4qPMdfdBJo75gLGjgnWpJRAjlr2Onftg=", "BYPiWsY7hVokG+NGE1Qj7zLUVRNJNypYvWHlHhF484p71ngMFwf/rDcbYJPRTtRIYE9tsTBHhjEanNSrT46tkOyNw9MB5DqfTYFBA4Vu9lLTZTDV7If8IH6UOs6y1krr3x6GwE+q4z89ZLRh9MJAlAFSCZO8q7fAFQGwzNfHUWI=", new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "barack_usa", 0 }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "CreationDate", "Description", "NumberOfPictures", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", 3, "Beatiful animals", 2 },
                    { 2, new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", 4, "Beatiful cities", 2 }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "Description", "PhotoPath", "Title", "TotalRating", "UploadDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals1.jpg", "Deer", 0.0, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "car1.jpg", "Car in woods", 0.0, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city4.jpg", "Japanese city", 0.0, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "ukraine1.jpg", "Kyiv", 0.0, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "ukraine3.jpg", "Field", 0.0, new DateTime(2018, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "ukraine6.jpg", "KPI", 0.0, new DateTime(2015, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "ukraine4.jpg", "Ukrainian hills", 0.0, new DateTime(2022, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 8, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "mountain1.jpg", "Japan mountain", 0.0, new DateTime(2019, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "nature1.jpg", "Hills", 0.0, new DateTime(2018, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals1.jpg", "Bird", 0.0, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 11, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals1.jpg", "Fox", 0.0, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 12, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals4.jpg", "Arctic Fox", 0.0, new DateTime(2017, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 13, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals5.jpg", "Fox", 0.0, new DateTime(2018, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 14, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals6.jpg", "Arctic fox", 0.0, new DateTime(2022, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 15, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals7.jpg", "Lion", 0.0, new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 16, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "animals8.jpg", "Wolf", 0.0, new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 17, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city1.jpg", "City and building", 0.0, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 18, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city2.jpg", "New York", 0.0, new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 19, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city3.jpg", "China, skyscrappers", 0.0, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 20, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city5.jpg", "Prague", 0.0, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 21, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city6.jpg", "New York busy streets", 0.0, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 22, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum", "city7.jpg", "London", 0.0, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "AlbumPhotos",
                columns: new[] { "Id", "AdditionDate", "AlbumId", "PhotoId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11 },
                    { 2, new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12 },
                    { 3, new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 },
                    { 4, new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 19 },
                    { 6, new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 20 },
                    { 7, new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 21 }
                });

            migrationBuilder.InsertData(
                table: "PhotoTags",
                columns: new[] { "Id", "PhotoId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 15, 1 },
                    { 3, 16, 1 },
                    { 4, 8, 3 },
                    { 5, 8, 5 },
                    { 6, 3, 3 },
                    { 7, 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "Comment", "PhotoId", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "Report comment 1", 6, 2, 2 },
                    { 2, "Report comment 2", 11, 2, 3 },
                    { 3, "Report comment 3", 18, 2, 3 },
                    { 4, "Report comment 4", 19, 0, 1 },
                    { 5, "Report comment 5", 21, 1, 1 },
                    { 6, "Report comment 6", 1, 2, 1 },
                    { 7, "Report comment 7", 8, 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlbumPhotos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AlbumPhotos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AlbumPhotos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AlbumPhotos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AlbumPhotos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AlbumPhotos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PhotoTags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
