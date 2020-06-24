﻿// <auto-generated />
using System;
using AuctionOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuctionOnline.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    partial class AuctionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuctionOnline.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            Fullname = "Admin",
                            IsBlocked = false,
                            Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG",
                            RoleId = 0,
                            Status = true,
                            Username = "admin123"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user1@gmail.com",
                            Fullname = "User 1",
                            IsBlocked = false,
                            Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG",
                            RoleId = 1,
                            Status = true,
                            Username = "user1"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user2@gmail.com",
                            Fullname = "User 2",
                            IsBlocked = false,
                            Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG",
                            RoleId = 1,
                            Status = true,
                            Username = "user2"
                        });
                });

            modelBuilder.Entity("AuctionOnline.Models.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentBid")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ItemId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("AuctionOnline.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Furniture"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Electric"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Smarts",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Laptops & Macs",
                            ParentId = 3
                        });
                });

            modelBuilder.Entity("AuctionOnline.Models.CategoryItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ItemId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryItems");
                });

            modelBuilder.Entity("AuctionOnline.Models.ExpiredItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CurrentDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ExpiredItems");
                });

            modelBuilder.Entity("AuctionOnline.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BidEndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("BidIncrement")
                        .HasColumnType("decimal(18,1)");

                    b.Property<DateTime?>("BidStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BidStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("MinimumBid")
                        .HasColumnType("decimal(18,1)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,1)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            BidEndDate = new DateTime(2020, 6, 23, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(4791),
                            BidIncrement = 2m,
                            BidStartDate = new DateTime(2020, 6, 20, 8, 45, 46, 728, DateTimeKind.Local).AddTicks(1378),
                            BidStatus = 1,
                            CreatedAt = new DateTime(2020, 6, 20, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(6752),
                            Description = "description 1",
                            MinimumBid = 7m,
                            Price = 5m,
                            Status = true,
                            Title = "product 1"
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 1,
                            BidEndDate = new DateTime(2020, 6, 23, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(9770),
                            BidIncrement = 2m,
                            BidStartDate = new DateTime(2020, 6, 20, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(9751),
                            BidStatus = 1,
                            CreatedAt = new DateTime(2020, 6, 20, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(9810),
                            Description = "description 2",
                            MinimumBid = 7m,
                            Price = 10m,
                            Status = true,
                            Title = "product 2"
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 1,
                            BidEndDate = new DateTime(2020, 6, 23, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(9852),
                            BidIncrement = 2m,
                            BidStartDate = new DateTime(2020, 6, 20, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(9850),
                            BidStatus = 1,
                            CreatedAt = new DateTime(2020, 6, 20, 8, 45, 46, 729, DateTimeKind.Local).AddTicks(9855),
                            Description = "description 3",
                            MinimumBid = 7m,
                            Price = 15m,
                            Status = true,
                            Title = "product 3"
                        });
                });

            modelBuilder.Entity("AuctionOnline.Models.Bid", b =>
                {
                    b.HasOne("AuctionOnline.Models.Account", "Account")
                        .WithMany("Bids")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionOnline.Models.Item", "Item")
                        .WithMany("Bids")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AuctionOnline.Models.Category", b =>
                {
                    b.HasOne("AuctionOnline.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("AuctionOnline.Models.CategoryItem", b =>
                {
                    b.HasOne("AuctionOnline.Models.Category", "Category")
                        .WithMany("CategoryItems")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionOnline.Models.Item", "Item")
                        .WithMany("CategoryItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuctionOnline.Models.ExpiredItem", b =>
                {
                    b.HasOne("AuctionOnline.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuctionOnline.Models.Item", b =>
                {
                    b.HasOne("AuctionOnline.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
