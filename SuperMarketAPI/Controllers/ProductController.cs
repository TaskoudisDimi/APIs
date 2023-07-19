using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperMarketAPI.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperMarketAPI.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllMembers()
        {

            var result = DataModel.Select<ProductTbl>();
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(result);
            return Ok(JSONresult);
        }

        [HttpGet("Id")]
        public ActionResult<Product> GetProductById(int id)
        {
            Connect dataById = new Connect();
            dataById.retrieveData("Select * From ProductTbl where ProdId = " + id);
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataById.table);
            return Ok(JSONresult);
        }

        [HttpGet("Name")]
        public ActionResult<Product> GetProductByName(string name)
        {
            Connect dataByName = new Connect();
            dataByName.retrieveData("Select * From ProductTbl where ProdName = '" + name + "'");
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataByName.table);
            return Ok(JSONresult);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Product> Create(Product product)
        {
            Connect dataPost = new Connect();
            dataPost.commandExc(@"Insert Into ProductTbl values('" + product.ProdName + "'," + product.ProdQty + "," + product.ProdPrice + "," + product.ProdCatID + ",'" + product.ProdCat + "','" + product.Date + "')");
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataPost.table);
            return Ok(JSONresult);
        }


        [HttpPut("Id")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Update(int id, Product product)
        {
            
            Connect dataUpdate = new Connect();
            dataUpdate.commandExc(@"Update ProductTbl set ProdName='" + product.ProdName + "', ProdQty='" + product.ProdQty + "', ProdPrice='" + product.ProdPrice + "', ProdCatID = '" + product.ProdCatID + "', ProdCat='" + product.ProdCat + "' Where Prodid = " + id);
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataUpdate.table);
            return Ok(JSONresult);
        }


        [HttpDelete("Id")]
        public ActionResult<Product> Delete(int id)
        {
            Connect dataDelete = new Connect();
            dataDelete.commandExc("Delete From ProductTbl where ProdId = " + id);
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataDelete.table);
            return Ok(JSONresult);
        }



        #region Token
        
        [HttpPost]
        public object Token()
        {
            Result<bool> result = new Result<bool>();

            return result;
           
            //string grantType = HttpContext.Current.Request.Form.Get("grant_type");
            //string clientID = HttpContext.Current.Request.Form.Get("client_id");
            //string clientSecret = HttpContext.Current.Request.Form.Get("client_secret");

            //if (string.IsNullOrWhiteSpace(grantType))
            //{
            //    result.SetResult(CodeEnum.ERROR, "grant_type must be specified.", false);
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
            //}

            //if (!grantType.Equals("client_credentials"))
            //{
            //    result.SetResult(CodeEnum.ERROR, "grant_type allowed values is client_credentials.", false);
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
            //}

            //if (string.IsNullOrWhiteSpace(clientID) || string.IsNullOrWhiteSpace(clientSecret))
            //{
            //    result.SetResult(CodeEnum.ERROR, "client_id and/or client_secret must be specified.", false);
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
            //}

            //List<IQ_AppSettings> modivcareClient = null;
            //if (useMobilityDB)
            //{
            //    modivcareClient = DataModel.Get<IQ_AppSettings>(filter: $" AND [IQ_AppSettings].[sett_name] = 'MODIVCARE_IQTAXI_CLIENT_ID' OR " +
            //    $"[IQ_AppSettings].[sett_name] = 'MODIVCARE_IQTAXI_CLIENT_SECRET'");
            //}
            //else
            //{
            //    string sql = "SELECT [sett_name], [sett_value] FROM [dbo].[radiotaxi_AppSettings] WHERE [radiotaxi_AppSettings].[sett_name] = 'MODIVCARE_IQTAXI_CLIENT_ID' " +
            //        "OR [radiotaxi_AppSettings].[sett_name] = 'MODIVCARE_IQTAXI_CLIENT_SECRET'";
            //    DataTable dt = DataModel.ExecTable(sql);
            //    modivcareClient = new List<IQ_AppSettings>();
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        modivcareClient.Add(new IQ_AppSettings()
            //        {
            //            sett_name = row["sett_name"].ToString(),
            //            sett_value = row["sett_value"].ToString()
            //        });
            //    }
            //}


            //string savedID = modivcareClient.Where(x => x.sett_name == "MODIVCARE_IQTAXI_CLIENT_ID").Select(x => x.sett_value).FirstOrDefault();
            //string savedSecret = modivcareClient.Where(x => x.sett_name == "MODIVCARE_IQTAXI_CLIENT_SECRET").Select(x => x.sett_value).FirstOrDefault();

            //if (!savedID.Equals(clientID) || !savedSecret.Equals(clientSecret))
            //{
            //    result.SetResult(CodeEnum.ERROR, "Wrong client_id or client_secret.", false);
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter());
            //}
            //string token = GetAuthToken(clientID, clientSecret);
            //return new
            //{
            //    access_token = token,
            //    token_type = "Bearer",
            //    expires_in = 2419200,
            //    scope = ""
            //};
        }


        public string GetAuthToken(string clientID, string clientSecret)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] idBytes = Encoding.ASCII.GetBytes(clientID);
            byte[] secretBytes = Encoding.ASCII.GetBytes(clientSecret);
            string token = Convert.ToBase64String(time.Concat(idBytes).Concat(secretBytes).ToArray());
            return EncryptedToken(token);
        }


        public string GetEncryptionKey()
        {
            return "EncryptionKey";
        }


        public bool AuthenticateToken(string token)
        {

            string decryptedToken = DecryptToken(token);
            byte[] data = Convert.FromBase64String(decryptedToken);
            DateTime date = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            return DateTime.UtcNow <= date.AddDays(28);
        }


        public string EncryptedToken(string token)
        {
            //GetEcryptToken

            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("EncryptedToken");
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(token);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public string DecryptToken(string encryptedToken)
        {

            //Get EncryptToken
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encryptedToken);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("TestDecretedToken");
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }

        }

        #endregion

    }
}
