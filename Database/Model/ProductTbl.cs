namespace SuperMarketAPI.Model
{
    public class ProductTbl
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public int ProdQty { get; set; }
        public int ProdPrice { get; set; }
        public int ProdCatID { get; set; }
        public string ProdCat { get; set; }
        public DateTime Date { get; set; }

    }
}
