using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<ApplicationRole> {

    public void Configure(EntityTypeBuilder<ApplicationRole> entity) {
        entity.ToTable("Roles", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("RolesHistory");
        }));
        // Define primary key
        entity.HasKey(e => e.Id)
            .HasName("PK_Security_Roles_Id");

        entity.Property(e => e.Id)
            .HasColumnName("Id")
            .UseIdentityColumn();
    }
}