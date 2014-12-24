using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Entities
{
    public partial class Slide
    {
        public int SlideId { get; set; }
        public string ImageName { get; set; }
        public Region Region { get; set; }
        public int Priority { get; set; }
        public string Caption { get; set; }
        public string Text { get; set; }
        public string Classes { get; set; }
    }
}