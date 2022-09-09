using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackAppAPI.Models
{
    public class Products
    {

        public int ProductId { get; set; }
        
        public string ProductName { get; set; }
        public int ProductQty { get; set; }
        public int ProductPrice { get; set; }
        public string ProductCat { get; set; }
        public DateTime ProductDate { get; set; }

    }
}