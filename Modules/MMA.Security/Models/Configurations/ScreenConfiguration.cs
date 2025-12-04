using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MMA.Security.Models;

public class ScreenConfiguration : IEntityTypeConfiguration<Screen> {

    public void Configure(EntityTypeBuilder<Screen> entity) {
        // Define table name, schema, and enable system-versioning (temporal table)
        entity.ToTable("Screens", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("ScreensHistory");
        }));

        entity.HasKey(e => e.Id).HasName("PK_security_Screens_Id");

        entity.Property(e => e.Id).HasColumnName("Id");
        entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(100);
        entity.Property(e => e.Url).HasColumnName("Url").HasMaxLength(500);
        entity.Property(e => e.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(500);
        entity.Property(e => e.IsActive).HasColumnName("IsActive").HasDefaultValue(true);

        entity.HasIndex(e => e.Id, "IX_security_Screens_Id");
    }
}