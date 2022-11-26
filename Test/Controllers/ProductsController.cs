using Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        List<int> test = new List<int>(); 
        Products products = new Products();
        string JSONString = string.Empty;
        
        [HttpGet]
        public ActionResult<IEnumerable<Products>> Get()
        {

            Connect con = new Connect();
            con.retrieveData("Select * from ProductTbl");
            JSONString = JsonConvert.SerializeObject(con.table);
            return Ok(JSONString);
        }

        [HttpGet("id")]
        public ActionResult<IEnumerable<Products>> GetById(int id)
        {
            Connect con = new Connect();
            con.retrieveData("Select * from ProductTbl where ProdId=" + id);
            JSONString = JsonConvert.SerializeObject(con.table);
            return Ok(JSONString);
        }


        [HttpPost]
        public ActionResult<IEnumerable<Products>> Create(Products products)
        {
            string query = @"Insert Into ProductTbl values('" + products.ProdName + "'," + products.ProdQty + "," + products.ProdPrice + ",'" + products.ProdCat + "','" + products.Date + "')";
            Connect con = new Connect();
            con.commandExc(query);
            JSONString = JsonConvert.SerializeObject(con.table);
            return Ok(JSONString);
        }

        [HttpPut("id")]
        public ActionResult<IEnumerable<Products>> Update(Products products)
        {
            string query = @"Update ProductTbl set values('" + products.ProdName + "'," + products.ProdQty + "," + products.ProdPrice + ",'" + products.ProdCat + "','" + products.Date + "') where ProdId = " + products.ProdId;
            Connect con = new Connect();
            con.commandExc(query);
            JSONString = JsonConvert.SerializeObject(con.table);
            return Ok(JSONString);
        }

        [HttpDelete("id")]
        public ActionResult<IEnumerable<Products>> Delete(Products products)
        {
            string query = @"Delete From ProductTbl where ProdId = " + products.ProdId;
            Connect con = new Connect();
            con.commandExc(query);
            JSONString = JsonConvert.SerializeObject(con.table);
            return Ok(JSONString);
        }

    }


}
