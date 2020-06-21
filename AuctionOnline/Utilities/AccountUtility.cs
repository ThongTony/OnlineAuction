using AuctionOnline.ViewModels;
using AuctionOnline.Models;
using System.Collections.Generic;

namespace AuctionOnline.Utilities
{
    public static class AccountUtility
    {
        public static Account MapVMToModel(AccountVM accountVM)
        {
            var model = new Account()
            {
                Id = accountVM.Id,
                Fullname = accountVM.Fullname,
                Username = accountVM.Username,
                Address = accountVM.Address,
                Status= accountVM.Status,
                Email = accountVM.Email,
                PhoneNumber = accountVM.PhoneNumber           
            };

            return model;
        }

        public static AccountVM MapModelToVM(Account account)
        {
            var viewModel = new AccountVM()
            {
                Id = account.Id,
                Fullname = account.Fullname,
                Username = account.Username,
                Address = account.Address,
                Status = account.Status,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber
            };

            return viewModel;
        }

        public static List<Account> MapVMsToModels(List<AccountVM> accountsVM)
        {
            var accounts = new List<Account>();
            foreach (var accountVM in accountsVM)
            {
                var account = new Account()
                {
                    Id = accountVM.Id,
                    Fullname = accountVM.Fullname,
                    Username = accountVM.Username,
                    Address = accountVM.Address,
                    Status = accountVM.Status,
                    Email = accountVM.Email,
                    PhoneNumber = accountVM.PhoneNumber
                };

                accounts.Add(account);
            }
            return accounts;
        }

        public static List<AccountVM> MapModelsToVMs(List<Account> accounts)
        {
            var accountsVM = new List<AccountVM>();
            foreach (var account in accounts)
            {
                var accountVM = new AccountVM()
                {
                    Id = account.Id,
                    Fullname = account.Fullname,
                    Username = account.Username,
                    Address = account.Address,
                    Status = account.Status,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber
                };
                accountsVM.Add(accountVM);
            }
            return accountsVM;
        }
    }
}
