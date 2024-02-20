using SampleAPI.Core;
using SampleAPI.Repository;
using SampleAPI.Types;

namespace SampleAPI.Middlewares;

public class PrepareCacheHostedService(IServiceProvider serviceProvider) : IHostedService
{
        
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a new scope to retrieve scoped services
        using var scope = serviceProvider.CreateScope();
        
        // Initializing the cache with existing data in database
        // Get the DbContext instance
        var pgSqlRepository = scope.ServiceProvider.GetRequiredService<PersonRepositoryPgSql>();
        await pgSqlRepository.LoadDataIntoCache(DbType.PgSql);

        var mySqlRepository = scope.ServiceProvider.GetRequiredService<PersonRepositoryMySql>();
        await mySqlRepository.LoadDataIntoCache(DbType.MySql);

        var queue = scope.ServiceProvider.GetRequiredService<AddPersonQueueV2>();
        queue.Start();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // noop
        return Task.CompletedTask;
    }
}