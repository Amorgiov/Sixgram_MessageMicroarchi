using System;
using System.Collections.Immutable;
using Message.Core.Dto;
using Message.Core.Services.Chat;
using Message.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Message.Core.Context
{
    public class ApplicationContext : DbContext
    {
        //public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatEntity>(chat =>
            {
                chat.Property(_ => _.Id).IsRequired();
                chat.Property(_ => _.Messages).HasMaxLength(int.MaxValue);
                chat.Property(_ => _.Members).HasMaxLength(int.MaxValue);
                chat.Property(_ => _.Admin).IsRequired();
            });
            
            builder.Entity<MessageEntity>(message =>
            {
                message.Property(_ => _.Id).IsRequired();
                message.Property(_ => _.Timestamp).IsRequired().ValueGeneratedOnAdd();
                message.Property(_ => _.ChatId).IsRequired();
                message.Property(_ => _.SenderId).IsRequired();
            });
        }
    }
}