namespace Auth.Errors;

public sealed class Forbidden: BaseError
{
    public Forbidden(int statusCode, string? description) : base(statusCode, description)
    {
    }

    public Forbidden(int statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static Forbidden Build(int statusCode, string? message, Exception? exception)
    {
        return exception != null
            ? new Forbidden(statusCode, message)
            : new Forbidden(statusCode, message, exception);
    }
}