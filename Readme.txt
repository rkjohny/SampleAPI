In file "appsettings.json" update the connection string for both MySql and PgSql
In file "Program.cs" in method "ConnectionMultiplexer.Connect" update the Redis connection parameter

In "Package manager Console" run the following commaned:

EntityFrameworkCore\Add-Migration InitialCreate -Context SampleAPI.Repository.PersonRepositoryPgSql
EntityFrameworkCore\Update-Database InitialCreate -Context SampleAPI.Repository.PersonRepositoryPgSql

EntityFrameworkCore\Add-Migration InitialCreate -Context SampleAPI.Repository.PersonRepositoryMySql
EntityFrameworkCore\Update-Database InitialCreate -Context SampleAPI.Repository.PersonRepositoryMySql
