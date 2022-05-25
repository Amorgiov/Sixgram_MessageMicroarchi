using Message.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Message.Database.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}