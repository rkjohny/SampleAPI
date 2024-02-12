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
        var cacheKey = dbType.GetDisplayName() + ":" + LogicalTable + ":" + person.Email;
        
        // First look into cache if found return
        var personInCache = cacheService.GetData<PersonDto>(cacheKey);
        if (personInCache != null)
        {
            return personInCache;
        }

        // Then look into database if not exists insert
        Person? personInDb = null;
        bool locAcquired = false;
        await Task.Factory.StartNew(() =>
        {
            try
            {
                locAcquired = MutexDb.WaitOne();
                if (locAcquired)
                {
                    personInDb = Items.FirstOrDefault(p => p.Email == person.Email);
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
        // TODO: set expiration to a valid value. (for now consider 10 hours as infinity)
        var expirationTime = DateTimeOffset.Now.AddMinutes(600);
        cacheService.SetData(cacheKey, newPersonInCache, expirationTime);

        return newPersonInCache;
    }

    public Task LoadDataIntoCache(DbType dbType)
    {
        foreach (var person in Items)
        {
            string cacheKey = dbType.GetDisplayName() + ":" + LogicalTable + ":" + person.Email;
            PersonDto newPersonInCache = new PersonDto(person);
            // TODO: set expiration to a valid value. (for now consider 10 hours as infinity)
            var expirationTime = DateTimeOffset.Now.AddMinutes(600);
            cacheService.SetData(cacheKey, newPersonInCache, expirationTime);
        }

        return Task.CompletedTask;
    }
}