using Microsoft.AspNetCore.Mvc;

namespace SuperMarketAPI.Controllers
{
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }



    }
}
