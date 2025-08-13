namespace Domain.Entities;

public class Guest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    public ICollection<ReservationGuest> ReservationGuests { get; set; } = new List<ReservationGuest>();
}