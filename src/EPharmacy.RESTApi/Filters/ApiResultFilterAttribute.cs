using Microsoft.AspNetCore.Mvc.Filters;

namespace EPharmacy.RESTApi.Filters;

public class APIResultFilterAttribute : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        await Task.Run(() => next());
    }
}