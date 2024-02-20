
namespace SampleAPI.Types;

public class AddPersonOutputV2(PersonDto personDto) : AbstractOutput
{ public PersonDto Person { get; set; } = personDto;
}
