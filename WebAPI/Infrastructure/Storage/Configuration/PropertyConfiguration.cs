using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.HasKey( p => p.Id );

        builder.Property( p => p.Name ).IsRequired().HasMaxLength( 50 );
        
        builder.Property(p => p.Country).IsRequired().HasMaxLength( 50 );
        
        builder.Property(p => p.City).IsRequired().HasMaxLength( 50 );
        
        builder.Property(p => p.Address).IsRequired().HasMaxLength( 50 );

        builder.Property( p => p.Latitude ).IsRequired().HasColumnType("decimal(12,10)");
        
        builder.Property(p => p.Longitude).IsRequired().HasColumnType("decimal(12,10)");
    }
}