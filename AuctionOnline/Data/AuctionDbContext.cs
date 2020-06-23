using AuctionOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuctionOnline.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }

        public DbSet<ExpiredItem> ExpiredItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId);


            modelBuilder.Entity<Bid>()
               .HasKey(bc => bc.Id);
            modelBuilder.Entity<Bid>()
                .HasOne(bc => bc.Item)
                .WithMany(b => b.Bids)
                .HasForeignKey(bc => bc.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Bid>()
               .HasOne(bc => bc.Account)
               .WithMany(c => c.Bids)
                .HasForeignKey(bc => bc.AccountId);

            modelBuilder.Entity<CategoryItem>()
                .HasKey(bc => new { bc.ItemId, bc.CategoryId });
            modelBuilder.Entity<CategoryItem>()
                .HasOne(bc => bc.Item)
                .WithMany(b => b.CategoryItems)
                .HasForeignKey(bc => bc.ItemId);
            modelBuilder.Entity<CategoryItem>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.CategoryItems)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Furniture", Level = 1 },
                new Category() { Id = 2, Name = "Living Room", Level = 2, ParentId = 1 },
                new Category() { Id = 3, Name = "Electric", Level = 1 },
                new Category() { Id = 4, Name = "Smarts", Level = 2, ParentId = 3 },
                new Category() { Id = 5, Name = "Laptops & Macs", Level = 2, ParentId = 3 },
                new Category() { Id = 6, Name = "Toys", Level = 1 },
                new Category() { Id = 7, Name = "Vehicles", Level = 1 },
                new Category() { Id = 8, Name = "Motor Bikes", Level = 2, ParentId = 7 },
                new Category() { Id = 9, Name = "Books", Level = 1 },
                new Category() { Id = 10, Name = "Accessories", Level = 1 },
                new Category() { Id = 11, Name = "Clothes", Level = 1 },
                new Category() { Id = 12, Name = "First Fashion", Level = 2, ParentId = 12 },
                new Category() { Id = 13, Name = "Phone", Level = 2, ParentId = 3 }
                );

            modelBuilder.Entity<Item>().HasData(
                new Item() { Id = 1, Title = "Máy lọc nước RO nóng lạnh Sunhouse SHR76210CK 10 lõi", Description = "description 1", Status = true, Photo = "MayLocNuoc.jpg", Document = "May-Loc-Nuoc.txt", AccountId = 2, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 2, Title = "Iphone 11 Promax ", Description = "description 2", Status = true, Photo = "11promax.jpg", Document = "iPhone-11-ProMax.txt", AccountId = 3, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 3, Title = "SamSung S20 Ultra", Description = "description 3", Status = true, Photo = "S20Ultra.jpg", Document = "SamSung-S20-Ultra.txt", AccountId = 4, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 4, Title = "Apple Watch Series 5", Description = "description 4", Status = true, Photo = "AppleWatchS5.jpg", Document = "Apple-Watch-S5.txt", AccountId = 2, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 5, Title = "Smart Tivi OLED LG 4K 55 inch 55C9PTA", Description = "description 5", Status = true, Photo = "tvoled55.jpg", Document = "Tv-Oled-LG.txt", AccountId = 3, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 6, Title = "Acer Predator 21 X", Description = "description 6", Status = true, Photo = "21x.jpg", Document = "21x.txt", AccountId = 4, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 7, Title = "Harley Davison S1000", Description = "description 7", Status = true, Photo = "s1000.jpg", Document = "s1000.txt", AccountId = 2, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 8, Title = "Lego Classic", Description = "description 8", Status = true, Photo = "lego.jpg", Document = "lego.txt", AccountId = 3, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 9, Title = "Thảm Lót Sàn", Description = "description 9", Status = true, Photo = "thamlotsan.jpg", Document = "thamlotsan.txt", AccountId = 4, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 10, Title = "Bàn Gỗ Thông ", Description = "description 10", Status = true, Photo = "bangothong.jpg", Document = "bangothong.txt", AccountId = 2, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now },
                new Item() { Id = 11, Title = "Sách cũ cần thanh lý", Description = "description 11", Status = true, Photo = "oldbook.jpg", Document = "oldbook.txt", AccountId = 3, BidStatus = 0, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddMinutes(3), MinimumBid = 2, BidIncrement = 3, CreatedAt = DateTime.Now }
                );

            modelBuilder.Entity<Account>().HasData(
                new Account() { Id = 1, Fullname = "Admin", Username = "admin123", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Address = "hcm", Email = "admin@gmail.com", PhoneNumber = 1234567890, Photo = "", RoleId = 0, IsBlocked = false, Status = true, CreatedAt = DateTime.Now },
                new Account() { Id = 2, Fullname = "Khoa", Username = "khoa", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Address = "hcm", Email = "khoa@gmail.com", PhoneNumber = 1234567890, Photo = "k.jpg", RoleId = 1, IsBlocked = false, Status = true, CreatedAt = DateTime.Now },
                new Account() { Id = 3, Fullname = "Thong", Username = "thong", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Address = "hcm", Email = "thong@gmail.com", PhoneNumber = 1234567890, Photo = "t.jpg", RoleId = 1, IsBlocked = false, Status = true, CreatedAt = DateTime.Now },
                new Account() { Id = 4, Fullname = "Phuc", Username = "phuc", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Address = "hcm", Email = "heykuteboy@gmail.com", PhoneNumber = 1234567890, Photo = "p.jpg", RoleId = 1, IsBlocked = false, Status = true, CreatedAt = DateTime.Now },
                new Account() { Id = 5, Fullname = "Vinh", Username = "vinh", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Address = "hcm", Email = "phuc@gmail.com", PhoneNumber = 1234567890, Photo = "v.jpg", RoleId = 1, IsBlocked = false, Status = true, CreatedAt = DateTime.Now }
                // password: admin123
                );


            modelBuilder.Entity<CategoryItem>().HasData(
                new CategoryItem() { CategoryId = 3, ItemId = 1, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 2, ItemId = 1, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 4, ItemId = 1, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 4, ItemId = 2, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 3, ItemId = 2, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 13, ItemId = 2, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 4, ItemId = 3, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 3, ItemId = 3, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 13, ItemId = 3, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 4, ItemId = 4, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 3, ItemId = 4, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 5, ItemId = 4, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 4, ItemId = 5, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 3, ItemId = 5, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 2, ItemId = 5, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 4, ItemId = 6, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 3, ItemId = 6, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 5, ItemId = 6, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 7, ItemId = 7, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 8, ItemId = 7, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 6, ItemId = 8, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 1, ItemId = 9, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 2, ItemId = 9, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 10, ItemId = 9, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 1, ItemId = 10, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 10, ItemId = 10, CreatedAt = DateTime.Now },
                new CategoryItem() { CategoryId = 9, ItemId = 11, CreatedAt = DateTime.Now }
                );

        }
    }
}
