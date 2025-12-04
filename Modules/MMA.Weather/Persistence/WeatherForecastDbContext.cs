using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using MMA.Weather.Models;
using Shared;
using System.Data;
using System.Reflection;

namespace MMA.Weather.Persistence;


public class WeatherForecastDbContext
    : DbContext
    , IWeatherForecastDbContext {
    private readonly ICurrentUserService _currentUserService;
    //private readonly IDomainEventService _domainEventService;

    public WeatherForecastDbContext(
        DbContextOptions<WeatherForecastDbContext> options,
        ICurrentUserService currentUserService
        //IDomainEventService domainEventService
        ) : base(options) {
        _currentUserService = currentUserService;
        //_domainEventService = domainEventService;
        Model = base.Model;
    }

    #region TABLES

    #region weather
    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }

    #endregion DBO

    #endregion TABLES

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>()) {
            if (!string.IsNullOrEmpty(_currentUserService.UserId)) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = int.Parse(_currentUserService.UserId!);
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = int.Parse(_currentUserService.UserId!);
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        break;
                }
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    public async Task<int> SaveChangesIgnoringAuditAsync(CancellationToken cancellationToken = new CancellationToken()) {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Ignore<DomainEvent>();  // Exclude from EF Core mapping

        #region security

        //builder.Entity<ApplicationUser>(entity => {
        //    entity.ToTable("Users", schema: "security");
        //});

        //builder.Entity<ApplicationRole>(entity => {
        //    entity.ToTable("Roles", schema: "security");
        //});

        //builder.Entity<ApplicationUserRole>(entity => {
        //    entity.ToTable("UserRoles", schema: "security");
        //    //entity.HasKey(r => new { r.UserId, r.RoleId });
        //});

        //builder.Entity<ApplicationUserClaim>(entity => {
        //    entity.ToTable("UserClaims", schema: "security");
        //});

        //builder.Entity<ApplicationUserLogin>(entity => {
        //    entity.ToTable("UserLogins", schema: "security");
        //    //entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        //});

        //builder.Entity<ApplicationRoleClaim>(entity => {
        //    entity.ToTable("RoleClaims", schema: "security");
        //});

        //builder.Entity<ApplicationUserToken>(entity => {
        //    entity.ToTable("UserTokens", schema: "security");
        //    //entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        //});

        #endregion security

        // Customize Identity table names
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //builder.ApplyAuditColumnOrder();
        // builder.AddRemovePluralizeConvention();
        builder.ApplyAuditColumnDefaultValue();
        builder.AddRemoveOneToManyCascadeConvention();
        builder.ApplyConventions();
    }

    public IDbConnection Connection => Database.GetDbConnection();
    public DatabaseFacade Db => Database;
    public IModel Model { get; }

    public EntityEntry Entry(object entity) => base.Entry(entity);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseLazyLoadingProxies(false);
        base.OnConfiguring(optionsBuilder);
    }

    private async Task DispatchEvents(DomainEvent[] events) {
        foreach (var @event in events) {
            @event.IsPublished = true;
            //await _domainEventService.Publish(@event);
            await Task.CompletedTask;
        }
    }
}
