using System.Collections.Generic;

namespace Message.Database.Models
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public List<int> Messages { get; set; }
        public List<int> Members { get; set; }
        public int Admin { get; set; }
    }
}