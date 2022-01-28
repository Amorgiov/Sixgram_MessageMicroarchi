using AutoMapper;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Database.Models;

namespace Message.Core.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ChatEntity, ChatDto>().ReverseMap();

            //Params not set (\_/)
            //               (•.•)
            //               ♥< \

            CreateMap<MessageEntity, MessageDto>().ReverseMap();
            
            CreateMap<ChatEntity, ResultContainer<ChatDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
        }
    }
}