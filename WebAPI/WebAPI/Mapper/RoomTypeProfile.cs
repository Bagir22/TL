using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapper;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<RoomType, ReadRoomTypeDto>();
        CreateMap<ReadRoomTypeDto, RoomType>()
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        CreateMap<RoomType, CreatedRoomTypeDto>();
        CreateMap<CreatedRoomTypeDto, RoomType>();

    }
}