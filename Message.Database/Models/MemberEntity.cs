using System;
using System.ComponentModel.DataAnnotations.Schema;
using Message.Common.Base;

namespace Message.Database.Models;

public class MemberEntity : BaseModel
{
    public Guid UserId { get; set; }
    
    [ForeignKey("ChatEntity")]
    public Guid ChatId { get; set; }
    
    public ChatEntity Chat { get; set; }
}