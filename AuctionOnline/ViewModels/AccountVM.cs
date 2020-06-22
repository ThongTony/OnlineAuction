using Microsoft.AspNetCore.Http;
using System;

namespace AuctionOnline.ViewModels
{
    public class AccountVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoName { get; set; }
        public Boolean Status { get; set; }
        public Boolean IsBlocked { get; set; }

    }
}
