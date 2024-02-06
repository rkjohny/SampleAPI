using SampleAPI.Models;

namespace SampleAPI.Types;

public class AddPersonOutput(Person person)
{
    // TODO: Do not send plain id in response, return an encoded value
    public long? Id { get; set; } = person.Id;
    public string? FirstName { get; set; } = person.FirstName;
    public string? LastName { get; set; } = person.LastName;
    public string? Email { get; set; } = person.Email;

    public DateTime? CreatedAt { get; set; } = person.CreatedAt;
    public DateTime? LastUpdatedAt { get; set; } = person.LastUpdatedAt;
}
