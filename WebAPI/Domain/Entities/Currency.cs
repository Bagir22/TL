namespace Domain.Entities;

public class Currency
{
    public int Id { get; set; }            
    public string Type { get; set; }     
    
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<RoomType> RoomTypes { get; set; } = new List<RoomType>();
}
