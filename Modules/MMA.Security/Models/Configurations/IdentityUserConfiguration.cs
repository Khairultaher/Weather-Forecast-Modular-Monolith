using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityUserConfiguration : IEntityTypeConfiguration<ApplicationUser> {

    public void Configure(EntityTypeBuilder<ApplicationUser> entity) {
        // Enable system-versioning (temporal table)
        entity.ToTable("Users", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("UsersHistory");
        }));

        // Define primary key
        entity.HasKey(e => e.Id)
            .HasName("PK_Security_Users_Id");

        entity.Property(e => e.Id)
            .HasColumnName("Id")
            .UseIdentityColumn();
    }
}