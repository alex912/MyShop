using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;
using System.Web.Caching;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace MyShop.Models
{
    public static class AccountManager
    {
        

        public static List<Account> Accounts
        {
            get
            {
                var cache = HttpContext.Current.Cache.Get("AccountCache") as List<Account>;
                if (cache != null)
                {
                    return cache;
                }

                UpdateCache();

                cache = HttpContext.Current.Cache.Get("AccountCache") as List<Account>;
                return cache;
            }
        }


        public static void UpdateCache()
        {
            if (HttpContext.Current.Cache.Get("AccountCache") != null)
            {
                HttpContext.Current.Cache.Remove("AccountCache");
            }

            var cache = MyShop.MyShopConfig.Dao.AccountsDAO.GetAllUsers().ToList();
            HttpContext.Current.Cache.Add("AccountCache", cache, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0), CacheItemPriority.Default, null);
        }

        public static bool LoginAs(string username, string password, bool rememberme)
        {
            var account = Accounts.FirstOrDefault(a => a.UserName == username);
            if (account == null)
            {
                return false;
            }

            var salt = account.Salt;
            var passhash = CalculateSHA256(password + salt, Encoding.Unicode);

            if (account.PassHash == passhash)
            {
                FormsAuthentication.RedirectFromLoginPage(username, rememberme);
                return true;
            }
            return false;
        }

        public static Account CreateAccount(string username, string password)
        {
            var account = new Account();
            account.UserName = username;
            account.Salt = CreateSalt(10);
            account.PassHash = CalculateSHA256(password + account.Salt, Encoding.Unicode);
            account.IpAddr = HttpContext.Current.Request.UserHostAddress;
            account.RegDate = DateTime.Now;
            account.Role = "User";
            account.Skype = "";
            account.WowInfo = new WowInfo() { BattleTag = "", DefaultCharacter = "", DefaultFraction = "", DefaultServer = ""};
            MyShopConfig.Dao.AccountsDAO.InsertAccount(account);
            UpdateCache();
            return account;
        }

        public static Account LoginAsGuest()
        {
            var account = new Account();
            account.UserName = "!guest-" + CreateSalt(10);
            account.Salt = "";
            account.PassHash = "";
            account.IpAddr = HttpContext.Current.Request.UserHostAddress;
            account.RegDate = DateTime.Now;
            account.Role = "Guest";
            account.Skype = "";
            account.WowInfo = new WowInfo() { BattleTag = "", DefaultCharacter = "", DefaultFraction = "", DefaultServer = "" };
            MyShopConfig.Dao.AccountsDAO.InsertAccount(account);
            UpdateCache();
            FormsAuthentication.SetAuthCookie(account.UserName, true);
            return account;
        }

        public static bool IsLoggedInAsUser()
        {
            var user = HttpContext.Current.User.Identity;
            if (!user.IsAuthenticated || user.Name.StartsWith("!guest-"))
            {
                return false;
            }

            return true;
        }

        private static string CreateSalt(int size)
        {

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }  

        private static string CalculateSHA256(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            SHA256CryptoServiceProvider cryptoTransformSHA256 = new SHA256CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA256.ComputeHash(buffer)).Replace("-", "");
        }

        public static bool TryToChangePassword(string currentpass, string newpass)
        {
            var account = Accounts.FirstOrDefault(a => a.UserName == HttpContext.Current.User.Identity.Name);
            if (account == null)
            {
                return false;
            }

            var salt = account.Salt;
            var passhash = CalculateSHA256(currentpass + salt, Encoding.Unicode);

            if (account.PassHash == passhash)
            {
                account.Salt = CreateSalt(10);
                account.PassHash = CalculateSHA256(newpass + account.Salt, Encoding.Unicode);
                UpdateAccount(account);
                return true;
            }
            return false;            
        }

        public static void UpdateAccount(Account account)
        {
            MyShopConfig.Dao.AccountsDAO.UpdateAccount(account);
            UpdateCache();
        }
    }
}