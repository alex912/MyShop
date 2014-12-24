using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace MyShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Lang), ErrorMessageResourceName="UserNameRequired")]
        [Display(Name="UserName", ResourceType=typeof(Resources.Lang))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType=typeof(Resources.Lang), ErrorMessageResourceName="PasswordRequired")]
        [Display(Name="Password", ResourceType=typeof(Resources.Lang))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Lang))]
        public bool RememberMe { get; set; }

    }
}