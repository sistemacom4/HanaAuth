using System.Net;

namespace Application.Errors;

public sealed class NotFoundError: BaseError
{
    private NotFoundError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    private NotFoundError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static NotFoundError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new NotFoundError(statusCode, message, exception)
            : new NotFoundError(statusCode, message);
    }
}