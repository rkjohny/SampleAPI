namespace SampleAPI.Types;

//TODO: Validate and Clean (string filed to prevent sql injections) input
public class AddPersonInput
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
}
