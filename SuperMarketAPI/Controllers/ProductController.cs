using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperMarketAPI.Model;

namespace SuperMarketAPI.Controllers
{
    //TODO: Token based
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllMembers()
        {
            Connect data = new Connect();
            data.retrieveData("Select * From ProductTbl");
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(data.table);
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



    }
}
