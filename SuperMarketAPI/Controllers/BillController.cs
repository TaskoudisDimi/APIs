using Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperMarketAPI.Model;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

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

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet]
        public IActionResult GetAllBills()
        {

            var result = DataModel.Select<BillTbl>();
            //string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(result);
        }


        [HttpGet("ByName/{name}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetCategoryByDate(DateTime Date)
        {
            var result = DataModel.Select<BillTbl>(where: $"{nameof(BillTbl.Date)} = '{Date}'");
            string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(JSONresult);
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<BillTbl> Create(BillTbl bill)
        {
            var result = DataModel.Create<BillTbl>(bill);
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
        [HttpPut]
        [ProducesResponseType(typeof(BillTbl), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BillTbl), StatusCodes.Status400BadRequest)]
        public ActionResult<BillTbl> Update([FromBody] BillTbl bill)
        {
            var result = DataModel.Update<BillTbl>(bill);
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
        public ActionResult<BillTbl> Delete([FromBody] BillTbl bill)
        {
            var result = DataModel.Delete<BillTbl>(bill);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}
