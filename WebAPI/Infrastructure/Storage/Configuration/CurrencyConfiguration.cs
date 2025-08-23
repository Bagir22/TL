using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure( EntityTypeBuilder<Currency> builder )
    {
        builder.HasKey( c => c.Id );

        builder.Property( c => c.Type )
            .IsRequired()
            .HasMaxLength( 50 );
    }
}