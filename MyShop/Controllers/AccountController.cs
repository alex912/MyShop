using MyShop.Models;
using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize(Roles="User,Admin")]
        public ActionResult Index()
        {
            var account = Profile["Account"] as Account;
            ViewBag.UserName = account.UserName;
            ViewBag.Skype = account.Skype;
            ViewBag.BattleTag = account.WowInfo.BattleTag;
            ViewBag.DefaultServer = account.WowInfo.DefaultServer;
            ViewBag.DefaultCharacter = account.WowInfo.DefaultCharacter;
            ViewBag.DefaultFraction = account.WowInfo.DefaultFraction;
            return View();
        }

        [Authorize(Roles="User,Admin")]
        public ActionResult Edit()
        {
            var model = new ChangePasswordModel() { Skype = (Profile["Account"] as Account).Skype};
            return View(model);
        }

        [Authorize(Roles="User,Admin")]
        [HttpPost]
        public ActionResult Edit(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (AccountManager.TryToChangePassword(model.CurrentPassword, model.Password))
                {
                    ViewBag.PasswordChanged = "Password changed!";
                    return View(model);
                } else
                {
                    ViewBag.PasswordChanged = "Current password is wrong!";
                    return View(model);
                }

            }
            return View(model);
        }

        [Authorize(Roles="User,Admin")]
        public ActionResult Wow()
        {
            var account = Profile["Account"] as Account;
            var model = new WowInfoModel() { BattleTag = account.WowInfo.BattleTag, DefaultServer = account.WowInfo.DefaultServer, DefaultCharacter = account.WowInfo.DefaultCharacter, DefaultFraction = account.WowInfo.DefaultFraction};
            return View(model);
        }

        [Authorize(Roles="User,Admin")]
        [HttpPost]
        public ActionResult Wow(WowInfoModel model)
        {
            if (ModelState.IsValid)
            {
                var account = Profile["Account"] as Account;
                account.WowInfo = new WowInfo() { BattleTag = model.BattleTag, DefaultServer = model.DefaultServer, DefaultCharacter = model.DefaultCharacter, DefaultFraction = model.DefaultFraction };
                AccountManager.UpdateAccount(account);
                ViewBag.Success = "Данные успешно сохранены";
                return View(model);
            }
            return View(model);
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            if (AccountManager.IsLoggedInAsUser())
            {
                return RedirectToAction("Index");
            }

            ViewBag.returnUrl = returnUrl;
            return View();
            
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (AccountManager.LoginAs(model.UserName, model.Password, model.RememberMe))
                {
                    
                    var url = string.IsNullOrWhiteSpace(returnUrl)
                        ? "~"
                        : returnUrl;

                    return Redirect(url);
                }
            }
            return View();
        }

        public ActionResult Logout(string returnUrl)
        {
            if (!AccountManager.IsLoggedInAsUser())
            {
                return RedirectToAction("Index", "Home");
            }

            FormsAuthentication.SignOut();
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            if (AccountManager.IsLoggedInAsUser())
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAccountModel model)
        {
            if (AccountManager.IsLoggedInAsUser())
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                AccountManager.CreateAccount(model.UserName, model.Password);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}