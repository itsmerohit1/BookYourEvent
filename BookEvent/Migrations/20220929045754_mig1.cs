using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEvent.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Time = table.Column<string>(nullable: false),
                    CommentAdded = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Time);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EmailId = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: true),
                    Duration = table.Column<float>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OtherDetails = table.Column<string>(nullable: true),
                    InviteByEmail = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    UserEmailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Title);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserEmailId",
                        column: x => x.UserEmailId,
                        principalTable: "Users",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    AssignedRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    InvitationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    EventTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.InvitationId);
                    table.ForeignKey(
                        name: "FK_Invitations_Events_EventTitle",
                        column: x => x.EventTitle,
                        principalTable: "Events",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserEmailId",
                table: "Events",
                column: "UserEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_EventTitle",
                table: "Invitations",
                column: "EventTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
