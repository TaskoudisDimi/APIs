using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperMarketAPI.Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Results;

namespace SuperMarketAPI.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        //[Consumes("application/x-www-form-urlencoded")]

        //// In your authentication service or login endpoint, generate the token:
        //string secretKey = "your_secret_key_here";
        //var tokenService = new TokenService(secretKey);
        //string token = tokenService.GenerateToken("user123", "john.doe", "Admin");

        //// In your authorization middleware or API endpoint, validate the token:
        //var tokenValidator = new TokenValidator(secretKey);
        //bool isValidToken = tokenValidator.ValidateToken(token);


        [HttpGet("jwt")]
        [Authorize]
        public IActionResult TestJWT()
        {
            return Ok("Authenticated using JWT token.");
        }

        [HttpGet("cookie")]
        [Authorize]
        public IActionResult TestCookie()
        {
            return Ok("Authenticated using Cookie token.");
        }


        private const string ValidUsername = "validUser";
        private const string ValidPassword = "validPassword";
        [HttpGet("Basic")]
        [BasicAuth] // Apply BasicAuthAttribute for authorization
        public IActionResult TestBasic()
        { 
            // Check if the Authorization header is present in the request
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized("Authorization header missing.");
            }

            // Retrieve the Authorization header value
            string authHeader = Request.Headers["Authorization"];

            // Check if the Authorization header is of type Basic
            if (!authHeader.StartsWith("Basic "))
            {
                return Unauthorized("Invalid authorization scheme.");
            }

            // Extract the Base64-encoded credentials
            string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();

            // Decode the Base64-encoded credentials
            byte[] decodedBytes = Convert.FromBase64String(encodedCredentials);
            string decodedCredentials = System.Text.Encoding.UTF8.GetString(decodedBytes);

            // Split the decoded credentials into username and password
            string[] credentials = decodedCredentials.Split(':', 2);

            // Check if the provided credentials match the expected username and password
            if (credentials.Length != 2 || credentials[0] != ValidUsername || credentials[1] != ValidPassword)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Authentication successful; return the secured resource
            return Ok("This is a secured resource.");
        }



        //Produces : It is a filter attribute that specifies the expected System.
        //Type the action will return and the supported response content types.
        //Consume: It is a filter attribute that specifies the supported request content types.

        //Request
        [Consumes("application/json")]
        //Response
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllProducts()
        {

            var result = DataModel.Select<ProductTbl>();
            //string JSONresult = JsonConvert.SerializeObject(result);

            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetProductById(int id)
        {
            var result = DataModel.Select<ProductTbl>(where: $"{nameof(ProductTbl.ProdId)} = '{id}'");
            return Ok(result);
        }

        [HttpGet("ByName/{name}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetProductByName(string name)
        {
            var result = DataModel.Select<ProductTbl>(where: $"{nameof(ProductTbl.ProdName)} = '{name}'");
            string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(JSONresult);
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ProductTbl> Create(ProductTbl product)
        {
            var result = DataModel.Create<ProductTbl>(product);
            if(result > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut]
        [ProducesResponseType(typeof(ProductTbl), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProductTbl), StatusCodes.Status400BadRequest)]
        public ActionResult<ProductTbl> Update([FromBody] ProductTbl product)
        {
            var result = DataModel.Update<ProductTbl>(product);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpDelete]
        public ActionResult<ProductTbl> Delete([FromBody] ProductTbl product)
        {
            var result = DataModel.Delete<ProductTbl>(product);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        #region Action Return Type

        ////The Web API action method can have following return types.
        ////        Void
        ////        Primitive type or Complex type (e.g. int, object, string, etc.)
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

        #region Token

        //[HttpPost]
        //public object Token()
        //{
        //    Result<bool> result = new Result<bool>();

        //    return result;

        //    //string grantType = HttpContext.Current.Request.Form.Get("grant_type");
        //    //string clientID = HttpContext.Current.Request.Form.Get("client_id");
        //    //string clientSecret = HttpContext.Current.Request.Form.Get("client_secret");

        //    //if (string.IsNullOrWhiteSpace(grantType))
        //    //{
        //    //    result.SetResult(CodeEnum.ERROR, "grant_type must be specified.", false);
        //    //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
        //    //}

        //    //if (!grantType.Equals("client_credentials"))
        //    //{
        //    //    result.SetResult(CodeEnum.ERROR, "grant_type allowed values is client_credentials.", false);
        //    //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
        //    //}

        //    //if (string.IsNullOrWhiteSpace(clientID) || string.IsNullOrWhiteSpace(clientSecret))
        //    //{
        //    //    result.SetResult(CodeEnum.ERROR, "client_id and/or client_secret must be specified.", false);
        //    //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
        //    //}

        //    //List<IQ_AppSettings> modivcareClient = null;
        //    //if (useMobilityDB)
        //    //{
        //    //    modivcareClient = DataModel.Get<IQ_AppSettings>(filter: $" AND [IQ_AppSettings].[sett_name] = 'MODIVCARE_IQTAXI_CLIENT_ID' OR " +
        //    //    $"[IQ_AppSettings].[sett_name] = 'MODIVCARE_IQTAXI_CLIENT_SECRET'");
        //    //}


        //    //string savedID = modivcareClient.Where(x => x.sett_name == "MODIVCARE_IQTAXI_CLIENT_ID").Select(x => x.sett_value).FirstOrDefault();
        //    //string savedSecret = modivcareClient.Where(x => x.sett_name == "MODIVCARE_IQTAXI_CLIENT_SECRET").Select(x => x.sett_value).FirstOrDefault();

        //    //if (!savedID.Equals(clientID) || !savedSecret.Equals(clientSecret))
        //    //{
        //    //    result.SetResult(CodeEnum.ERROR, "Wrong client_id or client_secret.", false);
        //    //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
        //    //}
        //    //string token = GetAuthToken(clientID, clientSecret);
        //    //return new
        //    //{
        //    //    access_token = token,
        //    //    token_type = "Bearer",
        //    //    expires_in = 2419200,
        //    //    scope = ""
        //    //};
        //}


        //public string GetAuthToken(string clientID, string clientSecret)
        //{
        //    byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        //    byte[] idBytes = Encoding.ASCII.GetBytes(clientID);
        //    byte[] secretBytes = Encoding.ASCII.GetBytes(clientSecret);
        //    string token = Convert.ToBase64String(time.Concat(idBytes).Concat(secretBytes).ToArray());
        //    return EncryptedToken(token);
        //}


        //public string GetEncryptionKey()
        //{
        //    return "EncryptionKey";
        //}


        //public bool AuthenticateToken(string token)
        //{

        //    string decryptedToken = DecryptToken(token);
        //    byte[] data = Convert.FromBase64String(decryptedToken);
        //    DateTime date = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
        //    return DateTime.UtcNow <= date.AddDays(28);
        //}


        //public string EncryptedToken(string token)
        //{
        //    //GetEcryptToken

        //    byte[] iv = new byte[16];
        //    byte[] array;
        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes("EncryptedToken");
        //        aes.IV = iv;
        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
        //                {
        //                    streamWriter.Write(token);
        //                }
        //                array = memoryStream.ToArray();
        //            }
        //        }
        //    }
        //    return Convert.ToBase64String(array);
        //}

        //public string DecryptToken(string encryptedToken)
        //{

        //    //Get EncryptToken
        //    byte[] iv = new byte[16];
        //    byte[] buffer = Convert.FromBase64String(encryptedToken);

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes("TestDecretedToken");
        //        aes.IV = iv;
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        //        using (MemoryStream memoryStream = new MemoryStream(buffer))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }

        //}

        #endregion

    }
}
