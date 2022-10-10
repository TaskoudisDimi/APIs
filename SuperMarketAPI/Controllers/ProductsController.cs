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
    [Log]
    public class ProductsController : ApiController
    {

        #region Action Return Type
        ////The Web API action method can have following return types.
        ////        Void
        ////        Primitive type or Complex type
        ////        HttpResponseMessage
        ////        IHttpActionResult

        ////Void
        //public void Delete(int id)
        //{
        //    //DeletePdocutFromDB(id);
        //}

        ////Primitive type or Complex type
        //public int GetProduct(int id)
        //{
        //    return id;
        //}

        ////HttpResponseMessage
        ////The advantage of sending HttpResponseMessage from an action method is that you can configure a response your way.
        //public HttpResponseMessage Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, id);
        //}

        ////IHttpActionResult
        ////You can create your own class that implements IHttpActionResult or use various methods of ApiController class
        ////that returns an object that implement the IHttpActionResult.
        //public IHttpActionResult Get(string name)
        //{
        //    if (name == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(name);
        //}



        ////Create Custom Return Type, Create class CreateCustomResultType
        //public IHttpActionResult GetName(string name)
        //{
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        return NotFound();
        //    }

        //    return new CreateCustomResultType(name, Request);

        //}



        #endregion


        #region Paramater Binding
        ////ApiController: Web API Controller Base Class, In ASP.NET Core we have Controller Base Class
        ////[System.Web.Http.HttpGet]: Specifies that an action supports the GET HTTP method.
        ////HttpResponseMessage: Represents a HTTP response message including the status code and data.
        ////[System.Web.Http.Route("api/market")]: Represents a route class for self-host (i.e. hosted outside of ASP.NET).
        ////either I have to name the method "Get" in order to handle GET requests or I have to add this:[System.Web.Http.HttpGet]
        //// GET: Department
        ////When something call route http://localhost:1234/api/market this method HANDLES Http Get request
        ////If Web API framework does not find matched routes for an incoming request then it will send 404 error response.
        ////Attribute routing ([System.Web.Http.Route("api/market")] )is supported in Web API 2. 
        ////In order to use attribute routing with Web API, it must be enabled in WebApiConfig by calling config.MapHttpAttributeRoutes() method.
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/market")]
        //public HttpResponseMessage GetValue()
        //{
        //    string query = @"Select * From ProductTbl";

        //    DataTable table = new DataTable();

        //    using (var con = new SqlConnection(ConfigurationManager.
        //        ConnectionStrings["smarketdb"].ConnectionString))
        //    using (var cmd = new SqlCommand(query, con))
        //    using (var adapter = new SqlDataAdapter(cmd))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        adapter.Fill(table);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, table);
        //}


        ////Query string parameter name and action method parameter name must be the same(case-insensitive).
        ////If names do not match, then the values of the parameters will not be set.The order of the parameters can be different.
        //[System.Web.Http.HttpGet]
        //public Products GetStudentById(Products ProdId)
        //{

        //    return ProdId;
        //}


        ////Multiple Primitive Parameters
        ////Query string parameter names must match with the name of an action method parameter. However, they can be in a different order.
        //public Products GetStudentByIdAndName(Products Id, Products Name)
        //{

        //    return Name;
        //}


        ////An HTTP POST request is used to create a new resource.
        ////It can include request data into the HTTP request body and also in the query string.
        ////[System.Web.Http.HttpPost]
        ////public string Post(Products dep)
        ////{
        ////    return "Updated Successfully";

        ////    //    try
        ////    //    {
        ////    //        string query = @"Insert Into Department Values('" + dep.DepartmentName + @"')";
        ////    //        DataTable table = new DataTable();
        ////    //        using (var con = new SqlConnection(ConfigurationManager.
        ////    //        ConnectionStrings["Employee"].ConnectionString))
        ////    //        using (var cmd = new SqlCommand(query, con))
        ////    //        using (var adapter = new SqlDataAdapter(cmd))
        ////    //        {
        ////    //            cmd.CommandType = CommandType.Text;
        ////    //            adapter.Fill(table);
        ////    //        }
        ////    //        return "Added Successfully";

        ////    //    }
        ////    //    catch
        ////    //    {
        ////    //        return "Failed to Add!";
        ////    //    }
        ////    //}

        ////}

        #endregion

        #region Media Type/ MIME type

        ////In HTTP request, MIME type is specified in the request header using Accept and Content-Type attribute.
        ////The Accept header attribute specifies the format of response data which the client expects and the Content-Type header attribute
        ////specifies the format of the data in the request body so that receiver can parse it into appropriate format.


        ////Web API includes built-in support for JSON, XML, BSON, and form-urlencoded data.
        ////It means it automatically converts request/response data into these formats OOB (out-of the box).

        ////Media type formatters are classes responsible for serializing request/response data so that Web API can understand the request data format and
        ////send data in the format which client expects.

        ////The following example demonstrates HTTP Get method that returns all built-in formatter classes.
        //public IEnumerable<string> Get()
        //{
        //    IList<string> formatters = new List<string>();

        //    foreach(var item in GlobalConfiguration.Configuration.Formatters)
        //    {
        //        formatters.Add(item.ToString());
        //    }


        //    //formatters.Add(GlobalConfiguration.Configuration.Formatters.JsonFormatter.GetType().FullName);
        //    //formatters.Add(GlobalConfiguration.Configuration.Formatters.XmlFormatter.GetType().FullName);
        //    //formatters.Add(GlobalConfiguration.Configuration.Formatters.FormUrlEncodedFormatter.GetType().FullName);

        //    return formatters.AsEnumerable<string>();
        //}

        ////BSON Formatter = Binary JSON

        ////JSON Formatter
        ////As mentioned above, Web API includes JsonMediaTypeFormatter class that handles JSON format.
        ////The JsonMediaTypeFormatter converts JSON data in an HTTP request into CLR objects (object in C# or VB.NET) and
        ////also converts CLR objects into JSON format that is embeded within HTTP response.

        ////Configure JSON Serialization

        ////XML Formatter
        ////The XmlMediaTypeFormatter class is responsible for serializing model objects into XML data.
        ////It uses System.Runtime.DataContractSerializer class to generate XML data.

        #endregion

        #region Web API Filter
        //Web API includes filters to add extra logic before or after action method executes.
        //Filters can be used to provide cross-cutting features such as logging, exception handling, performance measurement, authentication and authorization.
        //Every filter attribute class must implement IFilter interface included in System.Web.Http.Filters namespace.
        //However, System.Web.Http.Filters includes other interfaces and classes that can be used to create filter for specific purpose.

        #endregion



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/allproducts")]
        public HttpResponseMessage GetAllProducts()
        {
            string query = @"Select * From ProductTbl";
            Connect con = new Connect();
            con.retrieve_data(query);
            return Request.CreateResponse(HttpStatusCode.OK, con.table);
        }


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/products/{id}")]
        //public HttpResponseMessage GetValueById(int Id)
        //{

        //    string query = @"Select * From ProductTbl where Prodid=" + Id + "";
        //    Connect con = new Connect();
        //    con.retrieve_data(query);
        //    return Request.CreateResponse(HttpStatusCode.OK, con.table);
        
        //}

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/products/{name}")]
        //public HttpResponseMessage GetValueByName(string ProdName)
        //{

        //    string query = @"Select * From ProductTbl where ProdName=" + ProdName + "";
        //    Connect con = new Connect();
        //    con.retrieve_data(query);
        //    return Request.CreateResponse(HttpStatusCode.OK, con.table);

        //}

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/products")]
        public HttpResponseMessage PostProducts([FromBody]Products products)
        {
            string query = @"Insert Into ProductTbl values(" + products.Prodid + ",'" + products.ProdName + "'," + products.ProdQty + "," + products.ProdPrice + ",'" + products.ProdCat + "')";
            Connect con = new Connect();
            con.commandExc(query);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/delete/{Prodid}")]
        public HttpResponseMessage DeleteProducts([FromUri] int Prodid)
        {
            string query = @"Delete From ProductTbl where ProdId=" + Prodid;
            Connect con = new Connect();
            con.commandExc(query);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/put/{Prodid}")]
        public HttpResponseMessage PutProducts([FromUri] int Prodid, Products products)
        {
            string query = @"Update ProductTbl set ProdName='" + products.ProdName + "','" + products.ProdQty + "','" + products.ProdPrice + "','" + products.ProdCat + "' Where Prodid = " + products.Prodid;
            Connect con = new Connect();
            con.commandExc(query);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}





