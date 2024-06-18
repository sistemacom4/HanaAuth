namespace Auth.Errors;

public sealed class BadRequestError: BaseError
{
    private BadRequestError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private BadRequestError(int statusCode, string? description, Exception? innerException) : base(statusCode, description, innerException)
    {
    }

    public static BadRequestError Build(int statusCode, string? message, Exception? exception)
    {
        return exception != null
            ? new BadRequestError(statusCode, message)
            : new BadRequestError(statusCode, message, exception);
    }
}