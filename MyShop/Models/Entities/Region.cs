using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Entities
{
    public class Region
    {
        public string Name { get; set; }

        public string Language { get; set; }

        public string ImageFile { get; set; }

        public string Culture { get; set; }

        public override string ToString() 
        {
            return Name;
        }

        public delegate string OutputPriceDelegate(double price);

        public OutputPriceDelegate Price;
    }
}