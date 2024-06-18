namespace Auth.Models.Response;

public class ServiceLayerResponse<T>
{
    public bool IsSuccess { get; private set; } = false;
    public T? Data { get; private set; }
    
    public string? ErrorMessage { get; private set; }

    public static ServiceLayerResponse<T> Success(T data)
    {
        return new ServiceLayerResponse<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static ServiceLayerResponse<T> Fail(T? data, string? errorMessage = null)
    {
        return new ServiceLayerResponse<T>
        {
            Data = data,
            ErrorMessage = errorMessage
        };
    }
}