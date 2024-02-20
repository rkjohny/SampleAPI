namespace SampleAPI.Types;

public class AddPersonResponseV2(AddPersonRedisTaskV2 task)
{
    public string TrackingId { get; set; } = task.TrackingId;
    public string Firstname { get; set; } = task.Input.FirstName;
    public string Lastname { get; set; } = task.Input.LastName!;
    public string Email { get; set; } = task.Input.Email;
}