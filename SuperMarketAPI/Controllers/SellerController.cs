using Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperMarketAPI.Model;

namespace SuperMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        //[Consumes("application/x-www-form-urlencoded")]

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet]
        public IActionResult GetAllSellers()
        {

            var result = DataModel.Select<SellersTbl>();
            //string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetSellerById(int id)
        {
            var result = DataModel.Select<SellersTbl>(where: $"{nameof(SellersTbl.SellerId)} = '{id}'");
            return Ok(result);
        }

        [HttpGet("ByName/{name}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetSellerByName(string name)
        {
            var result = DataModel.Select<SellersTbl>(where: $"{nameof(SellersTbl.SellerName)} = '{name}'");
            string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(JSONresult);
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<SellersTbl> Create(SellersTbl seller)
        {
            var result = DataModel.Create<SellersTbl>(seller);
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
        [ProducesResponseType(typeof(SellersTbl), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SellersTbl), StatusCodes.Status400BadRequest)]
        public ActionResult<SellersTbl> Update([FromBody] SellersTbl seller)
        {
            var result = DataModel.Update<SellersTbl>(seller);
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
        public ActionResult<SellersTbl> Delete([FromBody] SellersTbl seller)
        {
            var result = DataModel.Delete<SellersTbl>(seller);
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
