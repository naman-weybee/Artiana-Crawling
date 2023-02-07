﻿// <auto-generated />
using Artiana_Crawling.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Artiana_Crawling.Migrations
{
    [DbContext(typeof(ArtianaWatchesDbContext))]
    partial class ArtianaWatchesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Artiana_Crawling.Data.WatchAuctions", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuctionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryTimeZone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_Watch_Auctions");
                });

            modelBuilder.Entity("Artiana_Crawling.Data.WatchDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AuctionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CaseMaterial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Circa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DimensionLength")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DimensionUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DimensionWidth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstimateEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstimateStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstimateUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefrenceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WatchArtist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WatchPaintingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinnigBid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinnigBidUnit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.ToTable("tbl_Watch_Details");
                });

            modelBuilder.Entity("Artiana_Crawling.Data.WatchDetails", b =>
                {
                    b.HasOne("Artiana_Crawling.Data.WatchAuctions", "Auction")
                        .WithMany()
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");
                });
#pragma warning restore 612, 618
        }
    }
}
