using SampleAPI.Types;

namespace SampleAPI.Core;

public abstract class AbstractHelper(IConfiguration configuration, ILogger<AddPersonHelper> logger)
{
    protected abstract void ValidateInput(AbstractInput input, object? args);

    protected abstract void CheckPermission(AbstractInput input, object? args);

    protected abstract Task<AbstractOutput> ExecuteHelperAsync(AbstractInput input, object? args);

    public async Task<AbstractOutput> ExecuteAsync(AbstractInput input, object? args)
    {
        try
        {
            CheckPermission(input, args);
            ValidateInput(input, args);
            return await ExecuteHelperAsync(input, args);
        }
        catch (Exception ex)
        {
            // Common place to error handling for all APIs
            logger.LogError(ex, "Failed to execute API: {StackTrace}", ex.StackTrace);
            throw;
        }
    }
}