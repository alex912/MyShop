using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace MyShop.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Lang), ErrorMessageResourceName = "CurrentPasswordRequired")]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Lang))]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Lang), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Lang))]
        [StringLength(64, ErrorMessageResourceName = "PasswordTooLong", ErrorMessageResourceType = typeof(Lang))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        [StringLength(50)]
        public string Skype { get; set; }
    }
}