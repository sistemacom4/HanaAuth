using System.Net;

namespace Application.Errors;

public class UnauthorizedError: BaseError
{
    private UnauthorizedError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    private UnauthorizedError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static UnauthorizedError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new UnauthorizedError(statusCode, message, exception)
            : new UnauthorizedError(statusCode, message);
    }
}