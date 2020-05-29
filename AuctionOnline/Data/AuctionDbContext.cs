using AuctionOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionOnline.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {
            
        }
        DbSet<Category> Categories { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountItem> AccountItems { get; set; }
        DbSet<BidIncrementDefinition> BidIncrementDefinitions { get; set; }
        DbSet<CategoryItem> CategoryItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<AccountItem>()
       .HasKey(bc => new { bc.ItemId, bc.AccountId });
            modelBuilder.Entity<AccountItem>()
                .HasOne(bc => bc.Item)
                .WithMany(b => b.AccountItems)
                .HasForeignKey(bc => bc.ItemId);
            modelBuilder.Entity<AccountItem>()
                .HasOne(bc => bc.Account)
                .WithMany(c => c.AccountItems)
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
                new Category() { Id = 2, Name = "Electric" }
                );

        }
    }
}
