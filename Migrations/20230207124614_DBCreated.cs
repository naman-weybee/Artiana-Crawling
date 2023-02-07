using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artiana_Crawling.Migrations
{
    public partial class DBCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Watch_Auctions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryTimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuctionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Watch_Auctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Watch_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WatchArtist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WatchPaintingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefrenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Circa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionLength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinnigBid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinnigBidCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Watch_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Watch_Details_tbl_Watch_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "tbl_Watch_Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Watch_Details_AuctionId",
                table: "tbl_Watch_Details",
                column: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Watch_Details");

            migrationBuilder.DropTable(
                name: "tbl_Watch_Auctions");
        }
    }
}
