using Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperMarketAPI.Model;

namespace SuperMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllMembers()
        {
            Connect data = new Connect();
            data.retrieveData("Select * From CategoryTbl");
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(data.table);
            return Ok(JSONresult);
        }

        [HttpGet("Id")]
        public ActionResult<Product> GetProductById(int id)
        {
            Connect dataById = new Connect();
            dataById.retrieveData("Select * From CategoryTbl where CatId = " + id);
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataById.table);
            return Ok(JSONresult);
        }

        [HttpGet("Name")]
        public ActionResult<Product> GetProductByName(string name)
        {
            Connect dataByName = new Connect();
            dataByName.retrieveData("Select * From CategoryTbl where CatName = '" + name + "'");
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataByName.table);
            return Ok(JSONresult);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Product> Create(Category category)
        {
            Connect dataPost = new Connect();
            dataPost.commandExc(@"Insert Into CategoryTbl values('" + category.CatName + "','" + category.CatDesc + "','" + category.Date + "')");
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataPost.table);
            return Ok(JSONresult);
        }


        [HttpPut("Id")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Update(int id, Category category)
        {

            Connect dataUpdate = new Connect();
            dataUpdate.commandExc(@"Update CategoryTbl set CatName='" + category.CatName + "', CatDesc='" + category.CatDesc + "', Date = '" + category.Date +  "' Where CatId = " + id);
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataUpdate.table);
            return Ok(JSONresult);
        }


        [HttpDelete("Id")]
        public ActionResult<Product> Delete(int id)
        {
            Connect dataDelete = new Connect();
            dataDelete.commandExc("Delete From CategoryTbl where CatId = " + id);
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dataDelete.table);
            return Ok(JSONresult);
        }

        [HttpGet]
        public IActionResult test()
        {
            
            return NotFound();
        }




    }
}
