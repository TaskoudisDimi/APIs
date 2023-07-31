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


        [Consumes("application/json")]
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
