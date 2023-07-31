using Database;
using System.Drawing;

namespace SuperMarketAPI.Model
{
    [TableName("ProductTbl")]
    public class SellersTbl
    {

        public int SellerId { get; set; }
        public string SellerUserName { get; set; }
        public string SellerPass { get; set; }
        public string SellerName { get; set; }
        public int SellerAge { get; set; }
        public int SellerPhone { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public Image image { get; set; }
        public DateTime Date { get; set; }

    }
}
