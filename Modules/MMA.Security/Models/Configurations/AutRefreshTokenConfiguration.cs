using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MMA.Security.Models;

public class AuthRefreshTokenConfiguration : IEntityTypeConfiguration<AuthRefreshToken> {

    public void Configure(EntityTypeBuilder<AuthRefreshToken> entity) {
        // Define table name, schema, and enable system-versioning (temporal table)
        entity.ToTable("AuthRefreshTokens", "security", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("AuthRefreshTokensHistory");
        }));

        entity.HasKey(e => e.Id).HasName("PK_NRC_AuthRefreshTokens_Id");

        entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();
        entity.Property(e => e.UserId).HasColumnName("UserId");
        entity.Property(e => e.RoleId).HasColumnName("RoleId");
        entity.Property(e => e.UserName).HasColumnName("UserName").HasMaxLength(100);
        entity.Property(e => e.RefreshToken).HasColumnName("RefreshToken").HasMaxLength(500);
        entity.Property(e => e.IssueDate).HasColumnName("IssueDate");
        entity.Property(e => e.ExpireDate).HasColumnName("ExpireDate");

        entity.HasIndex(e => e.Id, "IX_AuthRefreshToken_Id");
        entity.HasIndex(e => e.UserId, "IX_AuthRefreshToken_UserId");
    }
}