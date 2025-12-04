using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Security.Models;

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>> {

    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder) {
        builder.ToTable("UserTokens", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("UserTokensHistory");
        }));

        // Composite Primary Key
        //builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
    }
}