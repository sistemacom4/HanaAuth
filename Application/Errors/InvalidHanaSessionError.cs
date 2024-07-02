using System.Net;

namespace Application.Errors;

public sealed class InvalidHanaSessionError : BaseError
{
    public static readonly string DefaultMessage = "Sessão Hana expirou!";

    public InvalidHanaSessionError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    public InvalidHanaSessionError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static InvalidHanaSessionError Build(HttpStatusCode statusCode, string? message, Exception? exception = null)
    {
        return exception != null
            ? new InvalidHanaSessionError(statusCode, message, exception)
            : new InvalidHanaSessionError(statusCode, message);
    }
}