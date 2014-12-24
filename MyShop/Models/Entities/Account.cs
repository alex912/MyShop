using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Entities
{
    public class Account
    {
        public int UserId {get; set;}
        public string UserName {get; set;}
        public string PassHash {get; set;}
        public string Salt {get; set;}
        public DateTime RegDate {get; set;}
        public string Role {get; set;}
        public string IpAddr {get; set;}
        public int BaseDiscount {get; set;}
        public PaymentMethod DefaultPaymentMethod { get; set; }
        public string Skype { get; set; }
        public WowInfo WowInfo { get; set; }
    }

    public enum PaymentMethod 
    {
        PAYPAL = 0,
        WEBMONEY = 1,
    }

    public struct WowInfo
    {
        public string BattleTag { get; set; }
        public string DefaultServer { get; set; }
        public string DefaultCharacter { get; set; }
        public string DefaultFraction { get; set; }
    }
}