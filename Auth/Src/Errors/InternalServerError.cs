namespace Auth.Errors;

public sealed class InternalServerError: BaseError
{
    private InternalServerError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private InternalServerError(int statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static InternalServerError Build(int statusCode, string? message, Exception? exception)
    {
        return exception != null
            ? new InternalServerError(statusCode, message)
            : new InternalServerError(statusCode, message, exception);
    }
}