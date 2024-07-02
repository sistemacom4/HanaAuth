using System.Net;

namespace Application.Errors;

public sealed class EmployeeHasNotFoudError : BaseError
{
    public static readonly string DefaultMessage = "Nenhum colaborador encontrado!"; 
    
    public EmployeeHasNotFoudError(HttpStatusCode requestStatusCode, string? description) : base(requestStatusCode, description)
    {
    }

    public EmployeeHasNotFoudError(HttpStatusCode requestStatusCode, string? description, Exception? innerException) : base(requestStatusCode, description, innerException)
    {
    }

    public static EmployeeHasNotFoudError Build(HttpStatusCode statusCode, string message, Exception? exception = null)
    {
        return exception != null
            ? new EmployeeHasNotFoudError(statusCode, message, exception)
            : new EmployeeHasNotFoudError(statusCode, message);
    }
}