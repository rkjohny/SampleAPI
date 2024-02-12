
namespace SampleAPI.Types;

public class AddPersonOutput(PersonDto personDto) : AbstractOutput
{
    // TODO: Do not send plain id of Person in response, return an encoded value (say as string) so that it can be decoded (when it is received in another request)
    public PersonDto Person { get; set; } = personDto;
}
