using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>> {

    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder) {
        builder.ToTable("UserLogins", "security");

        builder.ToTable("UserLogins", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("UserLoginsHistory");
        }));

        // Composite Primary Key
        //builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

        // Optional: Ensure columns have appropriate sizes
        //builder.Property(ul => ul.LoginProvider)
        //       .HasMaxLength(250);

        //builder.Property(ul => ul.ProviderKey)
        //       .HasMaxLength(250);
    }
}