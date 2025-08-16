using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class RoomTypeAmenityConfiguration : IEntityTypeConfiguration<RoomTypeAmenity>
{
    public void Configure( EntityTypeBuilder<RoomTypeAmenity> builder )
    {
        builder.HasKey( rts => new
        {
            rts.RoomTypeId,
            rts.AmenityId
        } );

        builder.HasOne( rts => rts.RoomType )
            .WithMany( rt => rt.RoomTypeAmenities )
            .HasForeignKey( rs => rs.RoomTypeId );

        builder.HasOne( rts => rts.Amenity )
            .WithMany( s => s.RoomTypeAmenities )
            .HasForeignKey( rs => rs.AmenityId );
    }
}