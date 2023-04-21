using Microsoft.EntityFrameworkCore.Migrations;


namespace OnlineShop.Migrations
{
    public partial class RoleUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");


            object[] roleAdmin = new object[]
            {
                 Guid.NewGuid(),
                 "admin",
                 "مدیر"
            };

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: roleAdmin);

            object[] roleUser = new object[]
            {
                 Guid.NewGuid(),
                 "user",
                 "کاربر"
            };

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id","RoleName","RoleTitle" },
                values: roleUser);


            object[] adminUser = new object[]
            {
                Guid.NewGuid(),
                roleAdmin[0],
                "09112223344",
                Classes.Security.GetHash("8888"),
                1111
            }; 

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "RoleId", "Mobile", "Password", "Code" },
                values: adminUser);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
