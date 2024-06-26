using System.Net;

namespace Application.Errors;

public sealed class InternalServerError: BaseError
{
    private InternalServerError(HttpStatusCode statusCode, string? description) : base(statusCode, description)
    {
    }

    private InternalServerError(HttpStatusCode statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static InternalServerError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new InternalServerError(statusCode, message, exception)
            : new InternalServerError(statusCode, message);
    }
}