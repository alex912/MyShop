using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;

namespace MyShop.Models.DAL.Interfaces
{
    public interface IAccounts
    {
        IEnumerable<Account> GetAllUsers();
        Account GetUserById(int id);
        
        void UpdateAccount(Account acc);
        void DeleteAccount(Account acc);
        void InsertAccount(Account acc);
    }
}