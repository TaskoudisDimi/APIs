using Microsoft.AspNetCore.Mvc;
using TrackingAPI.Interfaces;
using TrackingAPI.Model;
using TrackingAPI.Repository;

namespace TrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMember members = new MembersRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetAllMembers()
        {
            return members.GetAllMember();
        }
        [HttpGet("id")]
        public ActionResult<Member> GetMemberById(int id)
        {
            return members.GetMember(id);
        }
       




    }
}
