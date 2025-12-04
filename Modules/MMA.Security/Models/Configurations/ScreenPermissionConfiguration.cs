using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMA.Security.Models;

namespace NRC.Infrastructure.Persistence.Configurations.Security;

public class ScreenPermissionConfiguration : IEntityTypeConfiguration<ScreenPermission> {

    public void Configure(EntityTypeBuilder<ScreenPermission> entity) {
        // Define table name, schema, and enable system-versioning (temporal table)
        entity.ToTable("ScreenPermissions", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("ScreenPermissionsHistory");
        }));

        entity.HasKey(e => e.Id).HasName("PK_security_ScreenPermission_Id");

        entity.Property(e => e.Id).HasColumnName("Id");
        entity.Property(e => e.RoleId).HasColumnName("RoleId");
        entity.Property(e => e.ScreenId).HasColumnName("ScreenId");
        entity.Property(e => e.IsActive).HasColumnName("IsActive").HasDefaultValue(true);

        entity.HasIndex(e => e.Id, "IX_security_ScreenPermission_Id");

        entity.HasOne(e => e.Screen)
            .WithMany(e => e.ScreenPermissions)
            .HasForeignKey(e => e.ScreenId)
            .HasConstraintName("FK_ScreenPermissions_Screens")
            .OnDelete(DeleteBehavior.NoAction);

        entity.HasOne(e => e.Role)
            .WithMany(e => e.ScreenPermissions)
            .HasForeignKey(e => e.RoleId)
            .HasConstraintName("FK_ScreenPermissions_Roles")
            .OnDelete(DeleteBehavior.NoAction);
    }
}