using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using SampleAPI.Core;
using SampleAPI.Models;
using SampleAPI.Types;
using DbType = SampleAPI.Types.DbType;

namespace SampleAPI.Repository;

public class AbstractPersonRepository<TC>(DbContextOptions<TC> options, ICacheService cacheService)
    : AbstractAuditableEntityRepository<TC, Person>(options) where TC : DbContext
{
    private static readonly Mutex MutexDb = new (false, typeof(TC).Name);
        
    private const string LogicalTable = "Person";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().ToTable("Person");
    }

    public async Task<PersonDto> AddIfNotExistsAsync(DbType dbType, Person person)
    {
        var cacheKey = Utils.GetCachedKey(dbType.GetDisplayName() + ":" + LogicalTable + ":" + person.Email);
        
        // First look into cache if found return
        var personInCache = cacheService.GetData<PersonDto>(cacheKey);
        if (personInCache != null)
        {
            return personInCache;
        }

        // Then look into database if not exists insert
        Person? personInDb = null;
        var locAcquired = false;
        await Task.Factory.StartNew(() =>
        {
            try
            {
                locAcquired = MutexDb.WaitOne();
                if (locAcquired)
                {
                    personInDb = Items.Cacheable(CacheExpirationMode.Sliding, Utils.ExpirationTimeSpan()).FirstOrDefault(p => p.Email == person.Email);
                    if (personInDb == null)
                    {
                        Items.Add(person);
                        SaveChanges();
                    }
                }
            }
            finally
            {
                if (locAcquired)
                {
                    MutexDb.ReleaseMutex();
                }
            }
        });


        if (!locAcquired)
        {
            throw new Exception("Could not perform database operation, error in acquiring lock for DB operation");
        }
        
        // Finally add into cache
        var newPersonInCache = new PersonDto(personInDb ?? person);
        cacheService.SetData(cacheKey, newPersonInCache, Utils.ExpirationDateTimeOffset());

        return newPersonInCache;
    }

    public Task LoadDataIntoCache(DbType dbType)
    {
        var persons = Items.Cacheable(CacheExpirationMode.Sliding, Utils.ExpirationTimeSpan());
        // Caching data returned by query
        foreach (var p in persons)
        {
            var cacheKey = Utils.GetCachedKey(dbType.GetDisplayName() + ":" + LogicalTable + ":" + p.Email);
            var newPersonInCache = new PersonDto(p);
            cacheService.SetData(cacheKey, newPersonInCache, Utils.ExpirationDateTimeOffset());
        }
        return Task.CompletedTask;
    }
}