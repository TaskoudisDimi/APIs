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
    public class CategoriesController : ApiController
    {
        

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categories")]
        public HttpResponseMessage GetAllProducts()
        {
            string query = @"Select * From CategoriesTbl";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);
        }

         
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categories/id")]
        public HttpResponseMessage GetValueById(int Id)
        {
            string query = @"Select * From CategoriesTbl where Catid=" + Id + "";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categories/name")]
        public HttpResponseMessage GetValueByName(string CatName)
        {
            string query = @"Select * From CategoriesTbl where CatName=" + CatName + "";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);
        }

    }
}   
