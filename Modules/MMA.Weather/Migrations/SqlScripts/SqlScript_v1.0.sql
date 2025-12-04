IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204111120_v1.0'
)
BEGIN
    IF SCHEMA_ID(N'weather') IS NULL EXEC(N'CREATE SCHEMA [weather];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204111120_v1.0'
)
BEGIN
    CREATE TABLE [weather].[WeatherForecast] (
        [Id] int NOT NULL IDENTITY,
        [Date] date NOT NULL,
        [TemperatureC] int NOT NULL,
        [Summary] nvarchar(250) NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_weather_WeatherForecast_Id] PRIMARY KEY ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [weather].[WeatherForecastHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204111120_v1.0'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251204111120_v1.0', N'10.0.0');
END;

COMMIT;
GO

