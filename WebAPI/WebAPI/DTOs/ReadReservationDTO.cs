using Domain.Entities;

namespace WebAPI.DTOs;

public class ReadReservationDTO
{
        public Guid PropertyID { get; set; }
        public Guid RoomTypeID { get; set; }
        public string ArrivalDateUTC { get; set; }
        public string DepartureDateUTC { get; set; }
        public string ArrivalUTC { get; set; }
        public string DepartureUTC { get; set; }
        public string GuestName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public decimal Total { get; set; }
        public Currency Currency { get; set; }
}