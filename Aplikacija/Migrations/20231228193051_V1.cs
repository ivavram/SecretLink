using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordToGuess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lenght = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Connect4Games",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    WinnerID = table.Column<int>(type: "int", nullable: true),
                    GameID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connect4Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Connect4Games_Players_WinnerID",
                        column: x => x.WinnerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "GuessTheWordGames",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordID = table.Column<int>(type: "int", nullable: true),
                    Connect4WinnerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuessTheWordGames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GuessTheWordGames_Connect4Games_Connect4WinnerID",
                        column: x => x.Connect4WinnerID,
                        principalTable: "Connect4Games",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_GuessTheWordGames_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOfPlayers = table.Column<int>(type: "int", nullable: false),
                    GameTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameWinnerID = table.Column<int>(type: "int", nullable: true),
                    PlayerTurnID = table.Column<int>(type: "int", nullable: true),
                    GuessTheWordID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_GuessTheWordGames_GuessTheWordID",
                        column: x => x.GuessTheWordID,
                        principalTable: "GuessTheWordGames",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Games_Players_GameWinnerID",
                        column: x => x.GameWinnerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerTurnID",
                        column: x => x.PlayerTurnID,
                        principalTable: "Players",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PlayerInGames",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInGames", x => new { x.GameID, x.PlayerID });
                    table.ForeignKey(
                        name: "FK_PlayerInGames_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerInGames_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connect4Games_GameID",
                table: "Connect4Games",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Connect4Games_WinnerID",
                table: "Connect4Games",
                column: "WinnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameWinnerID",
                table: "Games",
                column: "GameWinnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GuessTheWordID",
                table: "Games",
                column: "GuessTheWordID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerTurnID",
                table: "Games",
                column: "PlayerTurnID");

            migrationBuilder.CreateIndex(
                name: "IX_GuessTheWordGames_Connect4WinnerID",
                table: "GuessTheWordGames",
                column: "Connect4WinnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GuessTheWordGames_WordID",
                table: "GuessTheWordGames",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInGames_PlayerID",
                table: "PlayerInGames",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Connect4Games_Games_GameID",
                table: "Connect4Games",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connect4Games_Games_GameID",
                table: "Connect4Games");

            migrationBuilder.DropTable(
                name: "PlayerInGames");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GuessTheWordGames");

            migrationBuilder.DropTable(
                name: "Connect4Games");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
