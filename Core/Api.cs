using SampleAPI.Types;

namespace SampleAPI.Core;

public class Api (AddPersonHelper addPersonHelper)
{
    public async Task<AbstractOutput> AddPersonAsync(AbstractInput input, DbType dbType)
    {
        return await addPersonHelper.ExecuteAsync(input, dbType);
    }
}