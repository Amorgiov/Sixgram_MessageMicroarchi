using System;

namespace Message.Common.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}