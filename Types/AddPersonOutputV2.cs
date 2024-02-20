
namespace SampleAPI.Types;

public class AddPersonOutputV2(AddPersonRedisTaskV2 task, PersonDto personDto) : AbstractOutput
{
    public string TrackingId { get; set; } = task.TrackingId;
    public PersonDto Person { get; set; } = personDto;
}
