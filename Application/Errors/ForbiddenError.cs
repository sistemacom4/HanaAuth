using System.Net;

namespace Application.Errors;

public sealed class ForbiddenError: BaseError
{
    private ForbiddenError(HttpStatusCode statusCode, string? description) : base(statusCode, description)
    {
    }

    private ForbiddenError(HttpStatusCode statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static ForbiddenError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new ForbiddenError(statusCode, message, exception)
            : new ForbiddenError(statusCode, message);
    }
}