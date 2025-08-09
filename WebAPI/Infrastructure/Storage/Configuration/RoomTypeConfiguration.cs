using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Storage.Configuration;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    [Obsolete("Obsolete")]
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.HasKey( rt => rt.Id );
        
        builder.HasOne(rt => rt.Property)
            .WithMany(p => p.RoomTypes)
            .HasForeignKey(rt => rt.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property( rt => rt.Name ).IsRequired().HasMaxLength( 50 );
        
        builder.Property(rt => rt.DailyPrice).IsRequired().HasColumnType("decimal(9,7)");
        
        builder.Property(rt => rt.Currency)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(rt => rt.MinPersonCount).IsRequired();
            
        builder.Property( rt => rt.MaxPersonCount ).IsRequired();
        
        builder.Property(rt => rt.Services).HasMaxLength( 300 );
        
        builder.Property(rt => rt.Amenities).HasMaxLength( 300 );
        
        builder.HasCheckConstraint(
            "CK_RoomType_MinPersonCount_Positive",
            "[MinPersonCount] > 0"
        );

        builder.HasCheckConstraint(
            "CK_RoomType_MaxPersonCount_MoreOrEqualMinPersonCount",
            "[MaxPersonCount] >= [MinPersonCount]"
        );
    }
}