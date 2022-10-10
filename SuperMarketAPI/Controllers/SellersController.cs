using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Net.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;

namespace FullStackAppAPI.Controllers
{
    public class SellersController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/sellers")]
        public HttpResponseMessage GetAllProducts()
        {
            string query = @"Select * From SellerTbl";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/sellers/{id}")]
        public HttpResponseMessage GetValueById(int Id)
        {

            string query = @"Select * From SellerTbl where SellerId=" + Id + "";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/sellers/id")]
        public HttpResponseMessage GetValueByName(string SellerName)
        {

            string query = @"Select * From SellerTbl where SellerName=" + SellerName + "";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);

        }

    }
}