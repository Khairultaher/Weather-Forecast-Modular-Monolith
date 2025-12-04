using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using MMA.Security.Models;
using System.Data;

namespace MMA.Security.Persistence;

public interface ISecurityDbContext {

    #region TABLES

    #region Security

    DbSet<ApplicationUser> Users { get; set; }
    DbSet<ApplicationRole> Roles { get; set; }
    DbSet<ApplicationUserRole> UserRoles { get; set; }

    DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }
    DbSet<Screen> Screens { get; set; }
    DbSet<ScreenPermission> ScreenPermission { get; set; }

    #endregion Security

    #endregion TABLES

    IDbConnection Connection { get; }
    DatabaseFacade Db { get; }
    IModel Model { get; }

    EntityEntry Entry(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesIgnoringAuditAsync(CancellationToken cancellationToken);
}
