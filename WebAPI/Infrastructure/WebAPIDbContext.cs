using Infrastructure.Storage.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class WebAPIDbContext : DbContext
{
    public WebAPIDbContext() {}
    
    public WebAPIDbContext(DbContextOptions<WebAPIDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
    }
    
    // dotnet ef migrations add InitialMigration --project Infrastructure --startup-project WebAPI --output-dir Storage/Migrations
    // dotnet ef database update --project Infrastructure --startup-project WebAPI
}