using Microsoft.AspNetCore.Mvc;
using SuperMarketAPI.Model;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SuperMarketAPI.Controllers
{
    public class BillController : ControllerBase
    {
        public object Index()
        {
            Result<bool> result = new Result<bool>();

            return Ok();
        }

        public object GetBillById(int? id)
        {
            return Ok();
            
        }
        


    }
}
