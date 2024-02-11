using SampleAPI.Repository;
using SampleAPI.Types;

namespace SampleAPI.Middlewares;

public class PrepareCacheHostedService(IServiceProvider serviceProvider) : IHostedService
{
        
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a new scope to retrieve scoped services
        using var scope = serviceProvider.CreateScope();
            
        // Get the DbContext instance
        var pgSqlRepository = scope.ServiceProvider.GetRequiredService<PersonRepositoryPgSql>();
        await pgSqlRepository.LoadDataIntoCache(DbType.PgSql);

        var mySqlRepository = scope.ServiceProvider.GetRequiredService<PersonRepositoryMySql>();
        await mySqlRepository.LoadDataIntoCache(DbType.MySql);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // noop
        return Task.CompletedTask;
    }
}