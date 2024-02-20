using SampleAPI.Types;

namespace SampleAPI.Core;

public class Api (AddPersonHelper addPersonHelper, AddPersonQueueV2 addPersonQueue)
{
    public async Task<AbstractOutput> AddPersonAsync(AbstractInput input, DbType dbType)
    {
        return await addPersonHelper.ExecuteAsync(input, dbType);
    }


    public async Task<AddPersonResponseV2> AddPersonV2Async(AddPersonInput input, DbType dbType)
    {
        return new AddPersonResponseV2(await addPersonQueue.Enqueue(input, dbType));
    }
}