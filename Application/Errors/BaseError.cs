using System.Net;

namespace Application.Errors;

public abstract class BaseError : Exception
{
    private HttpStatusCode RequestStatusCode { get; }
    private string? Description { get; }
    private Exception? InnerException { get; }

    protected BaseError(HttpStatusCode requestStatusCode, string? description) : base(description)
    {
        RequestStatusCode = requestStatusCode;
        Description = description;
    }

    protected BaseError(HttpStatusCode requestStatusCode, string? description, Exception? innerException): base(description, innerException)
    {
        RequestStatusCode = requestStatusCode;
        Description = description;
        InnerException = innerException;
    }

    public override string Message => Description ?? base.Message;
    public HttpStatusCode StatusCode => RequestStatusCode;

    public void LogError()
    {
        Console.WriteLine($"Error {RequestStatusCode}: {Description}");
        if (InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {InnerException.Message}");
        }
    }
}