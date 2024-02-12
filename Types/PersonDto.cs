using SampleAPI.Models;

namespace SampleAPI.Types;

public class PersonDto
{
    public PersonDto(Person person)
    {
        Id = person.Id;
        FirstName = person.FirstName;
        LastName = person.LastName;
        Email = person.Email;
        CreatedAt = person.CreatedAt;
        LastUpdatedAt = person.LastUpdatedAt;
        SyncVersion = person.SyncVersion;
    }

    public PersonDto(PersonDto personDto)
    {
        Id = personDto.Id;
        FirstName = personDto.FirstName;
        LastName = personDto.LastName;
        Email = personDto.Email;
        CreatedAt = personDto.CreatedAt;
        LastUpdatedAt = personDto.LastUpdatedAt;
        SyncVersion = personDto.SyncVersion;
    }

    public long Id { get; set; }
        
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public long SyncVersion { get; set; }
}