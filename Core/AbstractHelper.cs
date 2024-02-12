using SampleAPI.Types;

namespace SampleAPI.Core
{
    public abstract class AbstractHelper(ILogger<AddPersonHelper> logger)
    {
        protected abstract void ValidateInput(AbstractInput input);

        protected abstract void CheckPermission(AbstractInput input);

        protected abstract Task<AbstractOutput> ExecuteAsync(AbstractInput input, object? args);

        public async Task<AbstractOutput> ExecuteHelperAsync(AbstractInput input, object? args)
        {
            try
            {
                ValidateInput(input);
                CheckPermission(input);
                return await ExecuteAsync(input, args);
            }
            catch (Exception ex)
            {
                // Common place to error handling for all APIs
                logger.LogError("Failed to execute API: " + ex.StackTrace);
                throw;
            }
        }
    }
}
