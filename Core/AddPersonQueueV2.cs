using SampleAPI.Types;
using StackExchange.Redis;

namespace SampleAPI.Core;

public class AddPersonQueueV2(RedisQueueV2 redisQueue, IServiceProvider serviceProvider, AddPersonNotifier notifier)
{
    private const string AddPersonTaskQueue = "AddPersonTaskQueue";

    private Timer? _timer;

    public async Task<AddPersonRedisTaskV2> Enqueue(AddPersonInput input, DbType dbType)
    {
        var task = new AddPersonRedisTaskV2
        {
            TrackingId = Guid.NewGuid().ToString(),
            Input = input,
            DbType = dbType
        };
        var taskJson = await Utils.Serialize(task);
        await redisQueue.PushAsync(AddPersonTaskQueue, taskJson);
        return task;
    }

    private async Task<AddPersonRedisTaskV2?> Dequeue()
    {
        var taskJson = await redisQueue.PopAsync(AddPersonTaskQueue);
        
        if (taskJson == RedisValue.Null || string.IsNullOrEmpty(taskJson)) return null;
        
        var task = await Utils.Deserialize<AddPersonRedisTaskV2>(taskJson!);
        return task;
    }

    private async void ProcessQueueItem(object? state)
    {
        if (state != null)
        {
            var task = (AddPersonRedisTaskV2)state;
            using var scope = serviceProvider.CreateScope();
            var helper = scope.ServiceProvider.GetRequiredService<AddPersonHelper>();
            var response = await helper.ExecuteAsync(task.Input, task.DbType);
            var output = (AddPersonOutput)response;

            var outputV2 = new AddPersonOutputV2(task, output.Person);
            await notifier.NotifyOnSuccessAsync(outputV2);
        }
    }
    
    public async void Process()
    {
        var task = await Dequeue();
        while (task != null)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessQueueItem), task);
            task = await Dequeue();
        }
    }

    public void ScheduleTaskWithFixedDelay(TimeSpan interval, Action task)
    {
        // Create and start the timer
        _timer = new Timer(_ =>
        {
            task.Invoke();
        }, null, TimeSpan.Zero, interval);
    }
    
    public void Start()
    {
        ScheduleTaskWithFixedDelay(TimeSpan.FromSeconds(1), Process);
    }
}