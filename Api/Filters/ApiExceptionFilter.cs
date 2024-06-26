using Application.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var result = new ObjectResult(context.Exception.Message);
        
        switch (context.Exception)
        {
            case BadRequestError:
                result.StatusCode = StatusCodes.Status400BadRequest;
                break;
            case NotFoundError:
                result.StatusCode = StatusCodes.Status404NotFound;
                break;
            case UnauthorizedError:
                result.StatusCode = StatusCodes.Status401Unauthorized;
                break;
            case ForbiddenError:
                result.StatusCode = StatusCodes.Status403Forbidden;
                break;
            default:
                _logger.LogError(context.Exception,  context.Exception.Message);
                result.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        context.Result = result;
    }
}