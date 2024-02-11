using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using SampleAPI.Core;
using SampleAPI.Models;
using SampleAPI.Types;
using DbType = SampleAPI.Types.DbType;

namespace SampleAPI.Repository;

public class AbstractPersonRepository<TC>(DbContextOptions<TC> options)
    : AbstractAuditableEntityRepository<TC, Person>(options) where TC : DbContext
{
    private const string LogicalTable = "Person";
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().ToTable("Person");
    }
    
    public async Task<PersonDto> AddIfNotExistsAsync(DbType dbType, Person person)
    {
        PersonCacheService cacheService = new PersonCacheService();

        string cacheKey = dbType.GetDisplayName() + ":" + LogicalTable + ":" + person.Email;
        
        // First look into cache if found return
        var personInCache = cacheService.GetData<PersonDto>(cacheKey);
        if (personInCache != null)
        {
            return personInCache;
        }
        
        // Then look into database, if not found, add into database
        var personInDb = Items.FirstOrDefault(p => p.Email == person.Email);
        if (personInDb == null)
        {
            Items.Add(person);
            await SaveChangesAsync();
        }
        
        // Finally add into cache
        PersonDto newPersonInCache = new PersonDto(personInDb ?? person);
        // TODO: set expiration to a valid value. (for now consider 10 hours as infinity)
        var expirationTime = DateTimeOffset.Now.AddMinutes(600);
        cacheService.SetData(cacheKey, newPersonInCache, expirationTime);
        return newPersonInCache;
    }

    public Task LoadDataIntoCache(DbType dbType)
    {
        PersonCacheService cacheService = new PersonCacheService();

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