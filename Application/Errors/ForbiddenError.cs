using System.Net;

namespace Application.Errors;

public sealed class ForbiddenError: BaseError
{
    private ForbiddenError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    private ForbiddenError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static ForbiddenError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new ForbiddenError(statusCode, message, exception)
            : new ForbiddenError(statusCode, message);
    }
}