using Microsoft.AspNetCore.Mvc;
using SampleAPI.Types;

namespace SampleAPI.Core
{
    public abstract class AbstractHelper()
    {
        protected abstract void ValidateInput(AbstractInput input);

        protected abstract void CheckPermission(AbstractInput input)
            ;

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
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
