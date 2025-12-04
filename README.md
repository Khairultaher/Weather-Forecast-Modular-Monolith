# MMA 
***
## SOLUTION ACHITECTURE
>This solution is make up of .NET 10 and it's latest features. Here EF Core is used as ORM to communicate with database system and as persistence storage(database) Azure SQL Server is in used.

|Architecture|Name|Details|
|----|-----|-------|
|Architecture Style|REST| REST APIs|
|Architecture Pattern| Clean Architecture|Clean Architecture with CQRS. All command(Insert,Update,Delete) and queries(Read) are separated for future scalability |

## SECURITY
>API Key & JWT Token both are in used for application security. Here API Key for authentication and JWT for user authorization.

|Item|Details|
|---------|-----|
|API Key  |For authentication all clients application that will consume the APIs need this API key|
|JWT Token|For authorization system will provide a JWT token and a Refresh token after a successful login. Both Token & Refresh token should have expire time. After the Token expire time system will provide a new token until the Refresh token is valid But after expire the Refresh token total session will be expired and need to re-login. System should ability to configure the Token & Refresh token expire time.

# Note for Developer
* *AppSettings.json*
	* After cloning the project, Copy and Paste appsettings.Production.json then rename it to appsettings.Development.json. 
Update all required values mentioned in this file Or collect the appsettings.Development.json file from dev team.
* *SQL Script*
	* Name the SQL script as per the release version, Ex-- SqlScript_v4.0.sql, SqlScript_v4.1.sql. file location must be there-- Source/External/NRC.Infrastructure/Migrations/Scripts


# EF Migrations
```
Add-Migration initial -c ApplicationDbContext -o Migrations/AspNetIdentity
Update-Database -Context ApplicationDbContext

remove-migration -Context ApplicationDbContext

add-migration v1_0 -Context ApplicationDbContext -o Migrations/Snapshots

Update-Database -Context ApplicationDbContext 

script-migration v1_0 v2_0 -c ApplicationDbContext -o source/External/NRC.Infrastructure/Migrations/Scripts/script_v2_0.sql -i

```
