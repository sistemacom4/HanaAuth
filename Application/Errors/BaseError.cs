using System.Net;

namespace Application.Errors;

public abstract class BaseError : Exception
{
    protected HttpStatusCode StatusCode { get; }
    protected string? Description { get; }
    protected Exception? InnerException { get; }

    protected BaseError(HttpStatusCode statusCode, string? description) : base(description)
    {
        StatusCode = statusCode;
        Description = description;
    }

    protected BaseError(HttpStatusCode statusCode, string? description, Exception? innerException): base(description, innerException)
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