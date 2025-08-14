using Domain.Entities;
using Infrastructure.Storage.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class WebAPIDbContext : DbContext
{
    public DbSet<Property> Property { get; set; }
    public DbSet<RoomType?> RoomType { get; set; }
    public DbSet<Reservation> Reservation { get; set; }
    public DbSet<Guest> Guest { get; set; }
    public DbSet<ReservationGuest> ReservationGuest { get; set; }
    public DbSet<Currency> Currency { get; set; }
    public DbSet<Amenity> Amenity { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<RoomTypeAmenity> RoomTypeAmenity { get; set; }
    public DbSet<RoomTypeService> RoomTypeService { get; set; }
    public WebAPIDbContext() {}
    
    public WebAPIDbContext(DbContextOptions<WebAPIDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationGuestConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new AmenityConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeAmenityConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeServiceConfiguration());
    }
    
    // dotnet ef migrations add InitialMigration --project Infrastructure --startup-project WebAPI --output-dir Storage/Migrations
    // dotnet ef database update --project Infrastructure --startup-project WebAPI
}