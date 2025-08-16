using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs.RoomTypeDTOs;

namespace WebAPI.Mapper;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<RoomType, CreatedRoomTypeDto>()
            .ForMember( dest => dest.Id, opt => opt.MapFrom( src => src.Id ) )
            .ForMember( dest => dest.PropertyId, opt => opt.MapFrom( src => src.PropertyId ) )
            .ForMember( dest => dest.CurrencyType,
                opt => opt.MapFrom( src => src.Currency.Type ) )
            .ForMember( dest => dest.Services,
                opt => opt.MapFrom( src => src.RoomTypeServices.Select( rts => rts.Service.Name ).ToList() ) )
            .ForMember( dest => dest.Amenities,
                opt => opt.MapFrom( src => src.RoomTypeAmenities.Select( rta => rta.Amenity.Name ).ToList() ) );

        CreateMap<RoomType, AvailableRoomTypeDTO>()
            .ForMember( d => d.RoomTypeId, opt => opt.MapFrom( s => s.Id ) )
            .ForMember( d => d.RoomTypeName, opt => opt.MapFrom( s => s.Name ) )
            .ForMember( d => d.CurrencyType, opt => opt.MapFrom( s => s.Currency.Type ) )
            .ForMember( d => d.Services, opt => opt.MapFrom( s => s.RoomTypeServices.Select( x => x.Service.Name ) ) )
            .ForMember( d => d.Amenities, opt => opt.MapFrom( s => s.RoomTypeAmenities.Select( x => x.Amenity.Name ) ) )
            .ForMember( d => d.Property, opt => opt.MapFrom( s => s.Property ) )
            .ForMember( d => d.AvailableRoomsCount, opt => opt.Ignore() );
    }
}