using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs.GuestDTOs;

namespace WebAPI.Mapper;

public class GuestProfile : Profile
{
    public GuestProfile()
    {
        CreateMap<GuestDTO, Guest>();
        CreateMap<Guest, GuestDTO>();

        CreateMap<ReservationGuest, GuestDTO>()
            .ForMember( dest => dest.Name, opt => opt.MapFrom( src => src.Guest.Name ) )
            .ForMember( dest => dest.PhoneNumber, opt => opt.MapFrom( src => src.Guest.PhoneNumber ) );
    }
}