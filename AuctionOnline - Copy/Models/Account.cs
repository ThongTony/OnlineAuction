using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Boolean Status { get; set; }
        public Boolean IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; }
        //public ICollection<AccountItem> AccountItems { get; set; }
    }
}
