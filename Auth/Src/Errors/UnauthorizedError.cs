namespace Auth.Errors;

public class UnauthorizedError: BaseError
{
    public UnauthorizedError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    public UnauthorizedError(int statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static UnauthorizedError Build(int statusCode, string? message, Exception? exception)
    {
        return exception != null
            ? new UnauthorizedError(statusCode, message)
            : new UnauthorizedError(statusCode, message, exception);
    }
}