using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class ReservationGuestConfiguration : IEntityTypeConfiguration<ReservationGuest>
{
    public void Configure(EntityTypeBuilder<ReservationGuest> builder)
    {
        builder.HasKey(rg => new { rg.ReservationId, rg.GuestId });

        builder.HasOne(rg => rg.Reservation)
            .WithMany(r => r.ReservationGuests)
            .HasForeignKey(rg => rg.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rg => rg.Guest)
            .WithMany(g => g.ReservationGuests)
            .HasForeignKey(rg => rg.GuestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}