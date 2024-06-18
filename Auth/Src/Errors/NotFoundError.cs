namespace Auth.Errors;

public sealed class NotFoundError: BaseError
{
    public NotFoundError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    public NotFoundError(int statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static NotFoundError Build(int statusCode, string? message, Exception? exception)
    {
        return exception != null
            ? new NotFoundError(statusCode, message)
            : new NotFoundError(statusCode, message, exception);
    }
}