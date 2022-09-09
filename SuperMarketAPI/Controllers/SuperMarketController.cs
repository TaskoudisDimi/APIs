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
    public class SuperMarketController : ApiController
    {
        //ApiController: Web API Controller Base Class, In ASP.NET Core we have Controller Base Class
        //[System.Web.Http.HttpGet]: Specifies that an action supports the GET HTTP method.
        //HttpResponseMessage: Represents a HTTP response message including the status code and data.
        //[System.Web.Http.Route("api/market")]: Represents a route class for self-host (i.e. hosted outside of ASP.NET).
        //either I have to name the method "Get" in order to handle GET requests or I have to add this:[System.Web.Http.HttpGet]
        // GET: Department
        //When something call route http://localhost:1234/api/market this method HANDLES Http Get request
        //If Web API framework does not find matched routes for an incoming request then it will send 404 error response.
        [System.Web.Http.HttpGet]
        //Attribute routing ([System.Web.Http.Route("api/market")] )is supported in Web API 2. 
        //In order to use attribute routing with Web API, it must be enabled in WebApiConfig by calling config.MapHttpAttributeRoutes() method.
        [System.Web.Http.Route("api/market")]
        public HttpResponseMessage GetValue()
        {
            string query = @"Select * From ProductTbl";

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

        
        //Query string parameter name and action method parameter name must be the same(case-insensitive).
        //If names do not match, then the values of the parameters will not be set.The order of the parameters can be different.
        [System.Web.Http.HttpGet]
        public Products GetStudentById(Products ProdId)
        {

            return ProdId;
        }


        //Multiple Primitive Parameters
        //Query string parameter names must match with the name of an action method parameter. However, they can be in a different order.
        public Products GetStudentByIdAndName(Products Id, Products Name)
        {

            return Name;
        }


        //An HTTP POST request is used to create a new resource.
        //It can include request data into the HTTP request body and also in the query string.
        //[System.Web.Http.HttpPost]
        //public string Post(Products dep)
        //{
        //    return "Updated Successfully";

        //    //    try
        //    //    {
        //    //        string query = @"Insert Into Department Values('" + dep.DepartmentName + @"')";
        //    //        DataTable table = new DataTable();
        //    //        using (var con = new SqlConnection(ConfigurationManager.
        //    //        ConnectionStrings["Employee"].ConnectionString))
        //    //        using (var cmd = new SqlCommand(query, con))
        //    //        using (var adapter = new SqlDataAdapter(cmd))
        //    //        {
        //    //            cmd.CommandType = CommandType.Text;
        //    //            adapter.Fill(table);
        //    //        }
        //    //        return "Added Successfully";

        //    //    }
        //    //    catch
        //    //    {
        //    //        return "Failed to Add!";
        //    //    }
        //    //}

        //}



    }
}