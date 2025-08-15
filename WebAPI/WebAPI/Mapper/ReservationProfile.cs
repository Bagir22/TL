using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs;
using WebAPI.DTOs.GuestDTOs;

namespace WebAPI.Mapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<string, DateOnly>().ConvertUsing(src => DateOnly.Parse(src));
            CreateMap<string, TimeOnly>().ConvertUsing(src => TimeOnly.Parse(src));

            CreateMap<ReadReservationDTO, Reservation>()
                .ForMember(dest => dest.ArrivalDateUTC, opt => opt.MapFrom(src => src.ArrivalDateUTC))
                .ForMember(dest => dest.DepartureDateUTC, opt => opt.MapFrom(src => src.DepartureDateUTC))
                .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => src.ArrivalTime))
                .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => src.DepartureTime))
                .ForMember(dest => dest.ReservationGuests, opt => opt.Ignore());
            
            CreateMap<CreatedReservationDTO, Reservation>();
            CreateMap<Reservation, CreatedReservationDTO>()
                .ForMember( dest => dest.Currency, opt => opt.MapFrom( src => src.RoomType.Currency.Type ) )
                .ForMember( dest => dest.RoomTypeID, opt => opt.MapFrom( src => src.RoomType.Id ) )
                .ForMember( dest => dest.ArrivalDateUTC,
                    opt => opt.MapFrom( src => src.ArrivalDateUTC.ToString( "yyyy-MM-dd" ) ) )
                .ForMember( dest => dest.DepartueDateUTC,
                    opt => opt.MapFrom( src => src.DepartureDateUTC.ToString( "yyyy-MM-dd" ) ) )
                .ForMember(dest => dest.ArrivalUTC, opt => opt.MapFrom(src => src.ArrivalTime.ToString("HH:mm")))
                .ForMember(dest => dest.DepartureUTC, opt => opt.MapFrom(src => src.DepartureTime.ToString("HH:mm")))
                .ForMember(dest => dest.Guests, opt => opt.MapFrom(src => src.ReservationGuests)); 
            
            CreateMap<Reservation, ReadReservationDTO>()
                .ForMember(dest => dest.Guests,
                    opt => opt.MapFrom(src => src.ReservationGuests.Select(rg => rg.Guest).ToList()));
            
            CreateMap<ReservationSearchFilterDTO, ReservationFilter>()
                .ForMember(dest => dest.Limit, opt => opt.MapFrom(src => src.Limit ?? 10))
                .ForMember(dest => dest.Offset, opt => opt.MapFrom(src => src.Offset ?? 0));
        }
    }
}