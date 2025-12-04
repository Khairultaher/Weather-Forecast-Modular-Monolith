using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole> {

    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder) {
        builder.ToTable("UserRoles", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("UserRolesHistory");
        }));

        // Composite Primary Key
        builder.Property(ur => ur.UserId);
        builder.Property(ur => ur.RoleId);
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        //Foreign keys
        builder.HasOne(x => x.User)
               .WithMany(x => x.UserRoles)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
               .WithMany(x => x.UserRoles)
               .HasForeignKey(x => x.RoleId);
    }
}