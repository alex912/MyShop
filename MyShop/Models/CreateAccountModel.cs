using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Resources;

namespace MyShop.Models
{
    public class CreateAccountModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Lang), ErrorMessageResourceName="UserNameRequired")]
        [Display(Name="UserName", ResourceType=typeof(Lang))]
        [StringLength(50, ErrorMessageResourceName="UserNameTooLong", ErrorMessageResourceType=typeof(Lang))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Lang), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Lang))]
        [StringLength(64, ErrorMessageResourceName = "PasswordTooLong", ErrorMessageResourceType = typeof(Lang))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
    }
}