using Microsoft.EntityFrameworkCore.Migrations;

namespace BookWorld.DAL.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    UserRelationId = table.Column<int>(type: "int", nullable: true),
                    BookRelationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBooks_Books_BookRelationId",
                        column: x => x.BookRelationId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBooks_Users_UserRelationId",
                        column: x => x.UserRelationId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name" },
                values: new object[,]
                {
                    { 1, "Paulo Coelho", "Simyacı" },
                    { 2, "Fyodor Dostoyevski", "Suç ve Ceza" },
                    { 3, "Sabahattin Ali", "Kürk Mantolu Madonna" },
                    { 4, "Albert Camus", "Yabancı" },
                    { 5, "Sabahattin Ali", "İçimizdeki Şeytan" },
                    { 6, "Oğuz Atay", "Tutunamayanlar" },
                    { 7, "Adam Fawer", "Olasılıksız" },
                    { 8, "Oğuz Atay", "Tehlikeli Oyunlar" },
                    { 9, "David Eagleman", "Incognito - Beynin Gizli Hayatı" },
                    { 10, "Kevin Guilfoile", "Klon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_BookRelationId",
                table: "UserBooks",
                column: "BookRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserRelationId",
                table: "UserBooks",
                column: "UserRelationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
