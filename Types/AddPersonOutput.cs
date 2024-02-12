using SampleAPI.Models;

namespace SampleAPI.Types;

public class AddPersonOutput(PersonDto personDto) : AbstractOutput
{
    public PersonDto Person { get; set; } = personDto;
}
