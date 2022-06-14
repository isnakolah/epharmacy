using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EPharmacy.Application.Common.Behaviours;

internal sealed class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public PerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IUserService userService)
    {
        (_timer, _logger, _currentUserService, _userService) = (new(), logger, currentUserService, userService);
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _userService.GetUserNameAsync(userId);
            }

            _logger.LogWarning("EPharmacy Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}