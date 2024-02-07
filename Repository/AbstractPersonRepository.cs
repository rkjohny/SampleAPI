using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using SampleAPI.Core;
using SampleAPI.Models;
using SampleAPI.Types;

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
        
        // First look into cache if found return
        string key = dbType.GetDisplayName() + ":" + LogicalTable + ":" + person.Email;
        var personInCache = cacheService.GetData<PersonDto>(key);
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
        // it will expire after 5 minutes
        var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        cacheService.SetData(key, newPersonInCache, expirationTime);
        return newPersonInCache;
    }
}