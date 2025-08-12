using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.HasKey( r => r.Id );

        builder.HasOne( r => r.Property )
            .WithMany( p => p.Reservations )
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne( r => r.RoomType )
            .WithMany( rt => rt.Reservations )
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.Property( r => r.ArrivalDateUTC ).IsRequired();

        builder.Property( r => r.DepartureDateUTC ).IsRequired();

        builder.Property( r => r.ArrivalTime ).IsRequired();

        builder.Property( r => r.DepartureTime ).IsRequired();

        builder.Property( r => r.GuestName ).IsRequired().HasMaxLength( 100 );

        builder.Property( r => r.GuestPhoneNumber ).IsRequired().HasMaxLength( 12 );

        builder.Property( rt => rt.Total ).IsRequired().HasColumnType( "decimal(9,2)" );

        builder.Property( rt => rt.Currency )
            .IsRequired()
            .HasConversion<int>();

        builder.HasCheckConstraint(
            "CK_Reservation_ArrivalBeforeDeparture",
            "CONVERT(DATETIME2, CONVERT(VARBINARY(6), DepartureTime) + CONVERT(BINARY(3), DepartureDateUTC)) > " +
            "CONVERT(DATETIME2, CONVERT(VARBINARY(6), ArrivalTime) + CONVERT(BINARY(3), ArrivalDateUTC))"
        );
    }
}