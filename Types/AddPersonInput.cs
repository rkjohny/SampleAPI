namespace SampleAPI.Types;

public class AddPersonInput
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
}
