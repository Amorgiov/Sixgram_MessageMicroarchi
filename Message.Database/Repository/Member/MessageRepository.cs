using Message.Database.Context;
using Message.Database.Models;
using Message.Database.Repository.Base;

namespace Message.Database.Repository.Member
{
    public class MemberRepository : BaseRepository<MemberEntity>, IMemberRepository
    {
        public MemberRepository(ApplicationContext context) : base(context) { }
    }
}