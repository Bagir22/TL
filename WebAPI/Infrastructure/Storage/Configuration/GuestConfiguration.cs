using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.PhoneNumber)
            .IsRequired()
            .HasMaxLength(12);
        
        builder.HasIndex(g => g.PhoneNumber)
            .IsUnique();
        
        builder.HasMany(g => g.ReservationGuests)
            .WithOne(rg => rg.Guest)
            .HasForeignKey(rg => rg.GuestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}