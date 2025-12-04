using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMA.Weather.Models.Configurations;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecastEntity> {


    public void Configure(EntityTypeBuilder<WeatherForecastEntity> entity) {
        // Define table name, schema, and enable system-versioning (temporal table)
        entity.ToTable("WeatherForecast", "weather", tb => tb.IsTemporal(ttb => {
            ttb.HasPeriodStart("ValidFrom");
            ttb.HasPeriodEnd("ValidTo");
            ttb.UseHistoryTable("WeatherForecastHistory", "weather");
        }));

        // Define primary key
        entity.HasKey(e => e.Id)
            .HasName("PK_weather_WeatherForecast_Id");

        //// Define properties
        entity.Property(e => e.Id)
            .HasColumnName("Id")
            .UseIdentityColumn();

        entity.Property(e => e.Date)
            .HasColumnName("Date")
            .IsRequired();

        entity.Property(e => e.TemperatureC)
            .HasColumnName("TemperatureC")
            .IsRequired();

        entity.Property(e => e.Summary)
            .HasColumnName("Summary")
            .HasMaxLength(250)
            .IsRequired(false);

        //// Define unique constraint
        //entity.HasIndex(e => e.RcUciCombo)
        //    .IsUnique()
        //    .HasDatabaseName("UQ_Master_Clients_RcUciCombo");

        //// Define foreign key relationships
        //entity.HasOne(e => e.RegionalCenter)
        //    .WithMany(e => e.Clients)
        //    .HasForeignKey(e => e.RegionalCenterId)
        //    .HasConstraintName("FK_Clients_RegionalCenters")
        //    .OnDelete(DeleteBehavior.NoAction);

        //entity.HasOne(e => e.RcPersonnel)
        //    .WithMany(e => e.Clients)
        //    .HasForeignKey(e => e.CaseManagerId)
        //    .HasConstraintName("FK_Clients_RcPersonnel")
        //    .OnDelete(DeleteBehavior.NoAction);

        //entity.HasOne(e => e.ActivityStatus)
        //    .WithMany()
        //    .HasForeignKey(e => e.ActivityStatusId)
        //    .HasConstraintName("FK_Clients_ActivityStatus")
        //    .OnDelete(DeleteBehavior.NoAction);
    }
}
