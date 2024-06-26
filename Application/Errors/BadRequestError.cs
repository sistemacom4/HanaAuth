using System.Net;

namespace Application.Errors;

public sealed class BadRequestError: BaseError
{
    private BadRequestError(HttpStatusCode statusCode, string? description) : base(statusCode, description)
    {
    }

    private BadRequestError(HttpStatusCode statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static BadRequestError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new BadRequestError(statusCode, message, exception)
            : new BadRequestError(statusCode, message);
    }
}