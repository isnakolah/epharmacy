﻿using EPharmacy.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EPharmacy.RESTApi.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(CustomException), HandleCustomException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            { typeof(HttpException), HandleHttpException }
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        type = type.BaseType == typeof(CustomException) ? type.BaseType : type;

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(context.Exception.Message, ServiceError.DefaultError);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        if (context.Exception is ValidationException exception)
        {
            var details = ServiceResult.Failed(exception.Errors, ServiceError.Validation);

            context.Result = new BadRequestObjectResult(details);
        }
        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var exception = new ValidateModelException(context.ModelState);

        context.Result = new BadRequestObjectResult(ServiceResult.Failed(exception.Errors, ServiceError.ValidationFormat));
        context.ExceptionHandled = true;
    }


    private void HandleCustomException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(ServiceError.CustomMessage(context.Exception.Message));

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleHttpException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(ServiceError.CustomMessage(context.Exception.Message));

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(
            ServiceError.CustomMessage(context.Exception is NotFoundException exception ? exception.Message : ServiceError.NotFound.ToString()));

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(ServiceError.ForbiddenError);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(ServiceError.ForbiddenError);

        context.Result = new UnauthorizedObjectResult(details);
        context.ExceptionHandled = true;
    }
}