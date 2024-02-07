using SampleAPI.Models;

namespace SampleAPI.Types;

public class AddPersonOutput(PersonDto personDto)
{
    public PersonDto Person { get; set; } = personDto;
}
