using System.Net;

namespace Application.Errors;

public sealed class BadRequestError: BaseError
{
    private BadRequestError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    private BadRequestError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static BadRequestError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new BadRequestError(statusCode, message, exception)
            : new BadRequestError(statusCode, message);
    }
}