﻿using Database;

namespace SuperMarketAPI.Model
{
    [TableName("ProductTbl")]
    public class CategoryTbl
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string CatDesc { get; set; }
        public DateTime Date { get; set; }


    }
}
