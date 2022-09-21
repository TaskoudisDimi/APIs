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
using FullStackAppAPI.Models;

namespace FullStackAppAPI.Controllers
{
    public class CategoriesController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categories")]
        public HttpResponseMessage GetAllProducts()
        {
            string query = @"Select * From CategoriesTbl";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["smarketdb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categories/id")]
        public HttpResponseMessage GetValue(int Id)
        {
            string query = @"Select * From CategoriesTbl where Catid=" + Id + "";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["smarketdb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    }
}