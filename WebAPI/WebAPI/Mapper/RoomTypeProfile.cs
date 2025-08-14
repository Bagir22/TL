using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs;
using WebAPI.DTOs.RoomTypeDTOs;

namespace WebAPI.Mapper;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<RoomType, ReadRoomTypeDto>();
        
        CreateMap<ReadRoomTypeDto, RoomType>()
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<CreatedRoomTypeDto, RoomType>();
        
        CreateMap<RoomType, CreatedRoomTypeDto>()
            .ForMember( dest => dest.Id, opt => opt.MapFrom( src => src.Id ) )
            .ForMember( dest => dest.PropertyId, opt => opt.MapFrom( src => src.PropertyId ) )
            .ForMember( dest => dest.CurrencyType,
                opt => opt.MapFrom( src => src.Currency.Type ) )
            .ForMember( dest => dest.Services,
                opt => opt.MapFrom( src => src.RoomTypeServices.Select( rts => rts.Service.Name ).ToList() ) )
            .ForMember( dest => dest.Amenities,
                opt => opt.MapFrom( src => src.RoomTypeAmenities.Select( rta => rta.Amenity.Name ).ToList() ) );

    }
}