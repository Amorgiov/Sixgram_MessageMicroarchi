using AutoMapper;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Message;
using Message.Core.Dto.Update;
using Message.Database.Models;

namespace Message.Core.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ChatEntity, ChatRequestDto>().ReverseMap();
            CreateMap<ChatEntity, ChatUpdateRequestDto>().ReverseMap();
            CreateMap<ChatEntity, ChatUpdateResponseDto>().ReverseMap();

            //Params not set (\_/)
            //               (•.•)
            //               ♥< \

            CreateMap<CreateMessageDto, MessageEntity>().ReverseMap();
            
            CreateMap<ChatEntity, ResultContainer<ChatUpdateRequestDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            CreateMap<ChatEntity, ResultContainer<ChatUpdateResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u)); 
            CreateMap<ChatEntity, ResultContainer<ChatResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            CreateMap<ChatEntity, ChatResponseDto>()
                .ForMember("ChatId", opt =>
                    opt.MapFrom(u => u.Id));
        }
    }
}