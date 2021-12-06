using System;

namespace Message.Database.Models.User
{
    public class MemberInfo
    {
        public int Id { get; set; }
        public int InvitedBy { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}