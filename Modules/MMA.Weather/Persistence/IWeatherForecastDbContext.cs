using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using MMA.Weather.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace MMA.Weather.Persistence;

public interface IWeatherForecastDbContext {

    #region TABLES

    #region weather
    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }

    #endregion weather

    #endregion TABLES

    IDbConnection Connection { get; }
    DatabaseFacade Db { get; }
    IModel Model { get; }

    EntityEntry Entry(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesIgnoringAuditAsync(CancellationToken cancellationToken);
}
