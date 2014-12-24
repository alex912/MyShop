using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class WowInfoModel
    {
        [RegularExpression("^[^#]+#[^#]+$")]
        public string BattleTag { get; set; }
        public string DefaultServer { get; set; }
        public string DefaultCharacter { get; set; }
        public string DefaultFraction { get; set; }
    }
}