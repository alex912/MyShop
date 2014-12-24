using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MyShop.Models.ViewModels
{
    public class AddToCartRequest
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
         
    }

    public class AddToCartResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}