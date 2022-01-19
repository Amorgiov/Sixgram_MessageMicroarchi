using AutoMapper;
using Message.Core.Dto.Chat;
using Message.Database.Models;

namespace Message.Core.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ChatEntity, ChatDto>();
            CreateMap<ChatDto, ChatEntity>();
        }
    }
}