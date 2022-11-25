using TrackingAPI.Interfaces;
using TrackingAPI.Model;

namespace TrackingAPI.Repository
{
    public class MembersRepository : IMember
    {
        List<Member> lisMembers = new List<Member>
        {
            new Member{MemberId=1, FirstName="Kirtesh", LastName="Shah", Address="Vadodara" },
            new Member{MemberId=2, FirstName="Nitya", LastName="Shah", Address="Vadodara" },
            new Member{MemberId=3, FirstName="Dilip", LastName="Shah", Address="Vadodara" },
            new Member{MemberId=4, FirstName="Atul", LastName="Shah", Address="Vadodara" },
            new Member{MemberId=5, FirstName="Swati", LastName="Shah", Address="Vadodara" },
            new Member{MemberId=6, FirstName="Rashmi", LastName="Shah", Address="Vadodara" },
        };
        public List<Member> GetAllMember()
        {
            return lisMembers;
        }

        public Member GetMember(int id)
        {
            return lisMembers.FirstOrDefault(x => x.MemberId == id);
        }

    }
}
