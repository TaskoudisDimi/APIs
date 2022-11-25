using TrackingAPI.Model;

namespace TrackingAPI.Interfaces
{
    public interface IMember
{
        List<Member> GetAllMember();
        Member GetMember(int id);

    }
}
