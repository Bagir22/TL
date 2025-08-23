using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class RoomTypeServiceConfiguration : IEntityTypeConfiguration<RoomTypeService>
{
    public void Configure( EntityTypeBuilder<RoomTypeService> builder )
    {
        builder.HasKey( rts => new
        {
            rts.RoomTypeId,
            rts.ServiceId
        } );

        builder.HasOne( rts => rts.RoomType )
            .WithMany( rt => rt.RoomTypeServices )
            .HasForeignKey( rs => rs.RoomTypeId );

        builder.HasOne( rts => rts.Service )
            .WithMany( s => s.RoomTypeServices )
            .HasForeignKey( rs => rs.ServiceId );
    }
}