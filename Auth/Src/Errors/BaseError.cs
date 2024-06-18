namespace Auth.Errors;

public abstract class BaseError : Exception
{
    protected int StatusCode { get; }
    protected string? Description { get; }
    protected Exception? InnerException { get; }

    protected BaseError(int statusCode, string? description) : base(description)
    {
        StatusCode = statusCode;
        Description = description;
    }

    protected BaseError(int statusCode, string? description, Exception? innerException): base(description, innerException)
    {
        StatusCode = statusCode;
        Description = description;
        InnerException = innerException;
    }

    public override string Message => Description ?? base.Message;

    public void LogError()
    {
        Console.WriteLine($"Error {StatusCode}: {Description}");
        if (InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {InnerException.Message}");
        }
    }
}