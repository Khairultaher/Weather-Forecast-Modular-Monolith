using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>> {

    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder) {
        builder.ToTable("RoleClaims", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("RoleClaimsHistory");
        }));
    }
}