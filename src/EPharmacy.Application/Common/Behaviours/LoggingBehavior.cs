using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace EPharmacy.Application.Common.Behaviours;

internal sealed class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IUserService userService)
    {
        (_logger, _currentUserService, _userService) = (logger, currentUserService, userService);
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        try
        {
            var userId = _currentUserService.UserId;

            var userName = await _userService.GetUserNameAsync(userId);

            _logger.LogInformation("EPharmacy Request: {Name} {@UserId} {@UserName} {@Request}", 
                requestName, userId, userName, request);
        }
        catch
        {
            if (!requestName.ToLower().Contains("login"))
                throw new UnauthorizedAccessException();

            _logger.LogWarning("EPharmacy Request: {Name} {@Request} user not logged in", 
                requestName, request);
        }
    }
}