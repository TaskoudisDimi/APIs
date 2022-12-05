using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarketAPI.Model;

namespace SuperMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllMembers()
        {
            Connect data = new Connect();
            data.retrieveData("Select * From ProductsTbl");
            return Ok(data.table);
        }

        [HttpGet("id")]
        public ActionResult<Product> GetProductById(int id)
        {
            Connect dataById = new Connect();
            dataById.retrieveData("Select * From ProductsTbl where ProdId = " + id);
            return Ok();
        }

        [HttpGet("name")]
        public ActionResult<Product> GetProductByName(string name)
        {
            Connect dataByName = new Connect();
            dataByName.retrieveData("Select * From ProductsTbl where ProdName = " + name);
            return Ok();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Product> Create(Product product)
        {
            Connect data3 = new Connect();
            data3.commandExc(@"Insert Into ProductTbl values(" + product.ProdId + ",'" + product.ProdName + "'," + product.ProdQty + "," + product.ProdPrice + ",'" + product.ProdCat + "','" + product.Date + "')");
            return Ok(data3.table);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Update(int id, Product product)
        {
            Connect data4 = new Connect();
            data4.commandExc(@"Update ProductTbl set ProdName='" + product.ProdName + "', ProdQty='" + product.ProdQty + "', ProdPrice='" + product.ProdPrice + "', ProdCat='" + product.ProdCat + "' Where Prodid = " + product.ProdId + "");
            return Ok(data4.table);
        }


        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            Connect data5 = new Connect();
            data5.commandExc("@Delete From ProductTbl where ProdId = " + id);
            return Ok(data5.table);
        }



    }
}
