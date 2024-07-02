using System.Net;

namespace Application.Errors;

public sealed class InvalidCredentialsError : BaseError
{
    public static readonly string DefaultMessage = "Credenciais Inválidas!";

    public InvalidCredentialsError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    public InvalidCredentialsError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static InvalidCredentialsError Build(HttpStatusCode statusCode, string message, Exception? exception = null)
    {
        return exception != null
            ? new InvalidCredentialsError(statusCode, message, exception)
            : new InvalidCredentialsError(statusCode, message);
    }
}