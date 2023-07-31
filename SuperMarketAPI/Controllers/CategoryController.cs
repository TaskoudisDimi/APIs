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

        //[Consumes("application/x-www-form-urlencoded")]

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {

            var result = DataModel.Select<CategoryTbl>();
            //string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetCategoryById(int id)
        {
            var result = DataModel.Select<CategoryTbl>(where: $"{nameof(CategoryTbl.CatId)} = '{id}'");
            return Ok(result);
        }

        [HttpGet("ByName/{name}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetCategoryByName(string name)
        {
            var result = DataModel.Select<CategoryTbl>(where: $"{nameof(CategoryTbl.CatName)} = '{name}'");
            string JSONresult = JsonConvert.SerializeObject(result);
            return Ok(JSONresult);
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CategoryTbl> Create(CategoryTbl category)
        {
            var result = DataModel.Create<CategoryTbl>(category);
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
        [ProducesResponseType(typeof(CategoryTbl), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CategoryTbl), StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryTbl> Update([FromBody] CategoryTbl category)
        {
            var result = DataModel.Update<CategoryTbl>(category);
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
        public ActionResult<CategoryTbl> Delete([FromBody] CategoryTbl category)
        {
            var result = DataModel.Delete<CategoryTbl>(category);
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
