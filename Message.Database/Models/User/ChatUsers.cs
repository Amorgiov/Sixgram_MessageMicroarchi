using System.Collections.Generic;

namespace Message.Database.Models.User
{
    public class ChatUsers
    {
        public int Count { get; set; }
        public IEnumerable<MemberInfo> Members { get; set; }
        //public IEnumerable<UserEntity> Users { get; set; }
    }
}