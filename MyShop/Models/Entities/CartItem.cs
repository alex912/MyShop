using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public Account Account{ get; set; }
        public Item Item { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}