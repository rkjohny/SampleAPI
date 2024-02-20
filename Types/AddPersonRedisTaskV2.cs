namespace SampleAPI.Types;

public class AddPersonRedisTaskV2 : AbstractRedisTaskV2
{
    public AddPersonInput Input { get; set; } = null!;

    public DbType DbType { get; set; }
}
