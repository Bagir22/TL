using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Property
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public ICollection<RoomType> RoomTypes { get; private set; } = new List<RoomType>();
    public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
    
    public Property(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Validate(name, country, city, address, latitude, longitude);

        Id = Guid.NewGuid();
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public void Update(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Validate(name, country, city, address, latitude, longitude);

        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    private static void Validate(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ValidationException("Name cannot be null or empty");
        }
            

        if (string.IsNullOrWhiteSpace(country))
        {
            throw new ValidationException( "Country cannot be null or empty" );
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ValidationException( "City cannot be null or empty" );
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ValidationException( "Address cannot be null or empty" );
        }

        if (latitude < -90  || latitude > 90)
        {
            throw new ValidationException( "Latitude must be between -90 and 90" );
        }

        if (longitude < -180  || longitude > 180)
        {
            throw new ValidationException( "Longitude must be between -180 and 180" );
        }
    }
}
