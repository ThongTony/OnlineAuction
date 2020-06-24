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
                new Category() { Id = 1, Name = "Furniture" },
                new Category() { Id = 2, Name = "Electric" },
                new Category() { Id = 3, Name = "Smarts", ParentId = 2 },
                new Category() { Id = 4, Name = "Laptops & Macs", ParentId = 3 }
                );

            modelBuilder.Entity<Item>().HasData(
                new Item() { Id = 1, Title = "product 1", Description = "description 1", Price = 5, BidStatus = 1, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddDays(3), MinimumBid = 7, CreatedAt = DateTime.Now, AccountId = 1, BidIncrement = 2, Status = true },
                new Item() { Id = 2, Title = "product 2", Description = "description 2", Price = 10, BidStatus = 1, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddDays(3), MinimumBid = 7, CreatedAt = DateTime.Now, AccountId = 1, BidIncrement = 2, Status = true },
                new Item() { Id = 3, Title = "product 3", Description = "description 3", Price = 15, BidStatus = 1, BidStartDate = DateTime.Now, BidEndDate = DateTime.Now.AddDays(3), MinimumBid = 7, CreatedAt = DateTime.Now, AccountId = 1, BidIncrement = 2, Status = true }
                );

            modelBuilder.Entity<Account>().HasData(
                new Account() { Id = 1, Fullname = "Admin", Username = "admin123", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Email = "admin@gmail.com", RoleId = 0, IsBlocked = false, Status = true },
                new Account() { Id = 2, Fullname = "User 1", Username = "user1", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Email = "user1@gmail.com", RoleId = 1, IsBlocked = false, Status = true },
                new Account() { Id = 3, Fullname = "User 2", Username = "user2", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Email = "user2@gmail.com", RoleId = 1, IsBlocked = false, Status = true }
                // password: admin123
                );

        }
    }
}
