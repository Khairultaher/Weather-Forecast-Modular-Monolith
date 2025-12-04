using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Security.Models.Configurations;


public class IdentityUserPasskeyConfiguration : IEntityTypeConfiguration<ApplicationUserPasskey> {
    public void Configure(EntityTypeBuilder<ApplicationUserPasskey> entity) {
        // Table with temporal table configuration
        entity.ToTable("UserPasskeys", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("UserPasskeysHistory");
        }));

        // Define primary key
        entity.HasKey(e => e.UserId)
            .HasName("PK_Security_UserPasskeys_UserId");

        entity.Property(e => e.UserId)
            .HasColumnName("UserId")
            .UseIdentityColumn();

        // Map the foreign key to ApplicationUser
        //entity.HasOne<ApplicationUser>()
        //      .WithMany(u => u.Passkeys)
        //      .HasForeignKey(p => p.UserId)
        //      .IsRequired();
    }
}

