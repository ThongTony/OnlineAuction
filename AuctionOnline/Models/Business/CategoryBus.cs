using AuctionOnline.Data;
using AuctionOnline.Models.Dao;
using AuctionOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionOnline.Models.Business
{
    public class CategoryBus
    {
        private AuctionDbContext db;
        public CategoryBus(AuctionDbContext _category)
        {
            db = _category;
        }
        public List<CategoryVM> GetAll()
        {
            return new CategoryDao(db).GetAll().Select(x => new CategoryVM {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                ParentId = x.ParentId,
                Parent = x.Parent,
                Children = x.Children
            }).ToList();
        }
    }
}
