using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<int>> {

    public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder) {
        builder.ToTable("UserClaims", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("UserClaimsHistory");
        }));
    }
}