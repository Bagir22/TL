using Domain.Entities;
using WebAPI.DTOs.GuestDTOs;

namespace WebAPI.DTOs;

public class CreatedReservationDTO
{
        public Guid Id { get; set; }
        public Guid PropertyID { get; set; }
        public Guid RoomTypeID { get; set; }
        public string ArrivalDateUTC { get; set; }
        public string DepartueDateUTC { get; set; }
        public string ArrivalUTC { get; set; }
        public string DepartureUTC { get; set; }
        public List<GuestDTO> Guests { get; set; } = new();
        public decimal Total { get; set; }
        public string Currency { get; set; }
}