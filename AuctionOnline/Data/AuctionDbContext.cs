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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Furniture" }, 
                new Category() { Id = 2, Name = "Electric" }
                );

        }
    }
}
