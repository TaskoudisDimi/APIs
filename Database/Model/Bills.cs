namespace SuperMarketAPI.Model
{
    public class Bills
    {

        public int BillId { get; set; }
        public string Comments { get; set; }
        public string SellerName { get; set; }
        public DateTime Date { get; set; }
        public int TotAmt { get; set; }


    }
}
