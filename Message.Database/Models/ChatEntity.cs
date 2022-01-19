﻿using System.Collections.Generic;
using Message.Common.Base;

namespace Message.Database.Models
{
    public class ChatEntity : BaseModel
    {
        public string Title { get; set; }
        public List<int> Messages { get; set; }
        public List<int> Members { get; set; }
        public int Admin { get; set; }
    }
}