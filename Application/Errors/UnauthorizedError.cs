using System.Net;

namespace Application.Errors;

public class UnauthorizedError: BaseError
{
    private UnauthorizedError(HttpStatusCode statusCode, string? description) : base(statusCode, description)
    {
    }

    private UnauthorizedError(HttpStatusCode statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static UnauthorizedError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new UnauthorizedError(statusCode, message, exception)
            : new UnauthorizedError(statusCode, message);
    }
}