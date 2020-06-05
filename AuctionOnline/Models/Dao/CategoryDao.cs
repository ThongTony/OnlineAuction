using AuctionOnline.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models.Dao
{
    public class CategoryDao
    {
        private AuctionDbContext db;
        public CategoryDao(AuctionDbContext _category)
        {
            db = _category;
        }
        public List<Category> GetAll( )
        {
            return db.Categories.ToList();
        }
    }
}
