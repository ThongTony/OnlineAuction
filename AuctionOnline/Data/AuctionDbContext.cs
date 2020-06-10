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
        public DbSet<AccountItem> AccountItems { get; set; }
        public DbSet<BidIncrementDefinition> BidIncrementDefinitions { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId);


            //modelBuilder.Entity<AccountItem>()
              //  .HasKey(bc => new { bc.ItemId, bc.AccountId });
            //modelBuilder.Entity<AccountItem>()
             //   .HasOne(bc => bc.Item)
             //   .WithMany(b => b.AccountItems)
            //    .HasForeignKey(bc => bc.ItemId);
            //modelBuilder.Entity<AccountItem>()
              //  .HasOne(bc => bc.Account)
             //   .WithMany(c => c.AccountItems)
             //   .HasForeignKey(bc => bc.AccountId);

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
                new Category() { Id = 2, Name = "Electric" }
                );
            modelBuilder.Entity<Account>().HasData(
                new Account() { Id = 1, Fullname = "Admin", Username = "admin123", Password = "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", Email = "admin@gmail.com", RoleId = 0, IsBlocked = false, Status = true } // password: admin123
                );

        }
    }
}
