using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<string, DateOnly>().ConvertUsing(src => DateOnly.Parse(src));
            CreateMap<string, TimeOnly>().ConvertUsing(src => TimeOnly.Parse(src));

            CreateMap<ReadReservationDTO, Reservation>();
            CreateMap<Reservation, ReadReservationDTO>();
            CreateMap<CreatedReservationDTO, Reservation>();
            CreateMap<Reservation, CreatedReservationDTO>();
        }
    }
}