using System.Net;

namespace Application.Errors;

public sealed class NotFoundError: BaseError
{
    private NotFoundError(HttpStatusCode statusCode, string? description) : base(statusCode, description)
    {
    }

    private NotFoundError(HttpStatusCode statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static NotFoundError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new NotFoundError(statusCode, message, exception)
            : new NotFoundError(statusCode, message);
    }
}