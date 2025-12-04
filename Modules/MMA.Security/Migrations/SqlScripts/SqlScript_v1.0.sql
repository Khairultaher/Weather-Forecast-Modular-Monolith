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
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    IF SCHEMA_ID(N'security') IS NULL EXEC(N'CREATE SCHEMA [security];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[AuthRefreshTokens] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] bigint NOT NULL,
        [RoleId] int NOT NULL,
        [UserName] nvarchar(100) NULL,
        [RefreshToken] nvarchar(500) NOT NULL,
        [IssueDate] datetime2 NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_NRC_AuthRefreshTokens_Id] PRIMARY KEY ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[AuthRefreshTokensHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[Roles] (
        [Id] int NOT NULL IDENTITY,
        [DisplayName] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Security_Roles_Id] PRIMARY KEY ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[RolesHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[Screens] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Url] nvarchar(500) NOT NULL,
        [ImageUrl] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_security_Screens_Id] PRIMARY KEY ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[ScreensHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[UserPasskeys] (
        [UserId] int NOT NULL IDENTITY,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [CredentialId] varbinary(max) NOT NULL,
        CONSTRAINT [PK_Security_UserPasskeys_UserId] PRIMARY KEY ([UserId]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[UserPasskeysHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [MiddleName] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Security_Users_Id] PRIMARY KEY ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[UsersHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] int NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [security].[Roles] ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[RoleClaimsHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[ScreenPermissions] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] int NOT NULL,
        [ScreenId] int NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_security_ScreenPermission_Id] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ScreenPermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [security].[Roles] ([Id]),
        CONSTRAINT [FK_ScreenPermissions_Screens] FOREIGN KEY ([ScreenId]) REFERENCES [security].[Screens] ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[ScreenPermissionsHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[UserClaimsHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[UserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] int NOT NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[UserLoginsHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[UserRoles] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] int NULL,
        [CreatedAt] datetime2 NULL,
        [LastModifiedBy] int NULL,
        [LastModifiedAt] datetime2 NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [security].[Roles] ([Id]),
        CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[UserRolesHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE TABLE [security].[UserTokens] (
        [UserId] int NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
        [ValidTo] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
        CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]),
        PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
    ) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [security].[UserTokensHistory]));
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_AuthRefreshToken_Id] ON [security].[AuthRefreshTokens] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_AuthRefreshToken_UserId] ON [security].[AuthRefreshTokens] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_RoleClaims_RoleId] ON [security].[RoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [security].[Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_ScreenPermissions_RoleId] ON [security].[ScreenPermissions] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_ScreenPermissions_ScreenId] ON [security].[ScreenPermissions] ([ScreenId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_security_ScreenPermission_Id] ON [security].[ScreenPermissions] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_security_Screens_Id] ON [security].[Screens] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_UserClaims_UserId] ON [security].[UserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_UserLogins_UserId] ON [security].[UserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [IX_UserRoles_RoleId] ON [security].[UserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [security].[Users] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [security].[Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251204120642_v1.0'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251204120642_v1.0', N'10.0.0');
END;

COMMIT;
GO

