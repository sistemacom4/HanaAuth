using System.Net;

namespace Application.Errors;

public sealed class InternalServerError: BaseError
{
    private InternalServerError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    private InternalServerError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static InternalServerError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new InternalServerError(statusCode, message, exception)
            : new InternalServerError(statusCode, message);
    }
}