using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackAppAPI.Models
{
    public class Sellers
    {
        public int SellerId  { get; set; }

        public string SellerName { get; set; }

        public int SellerAge { get; set; }

        public int SellerPhone { get; set; }

        public int SellerPass { get; set; }

        public DateTime Date { get; set; }

    }
}