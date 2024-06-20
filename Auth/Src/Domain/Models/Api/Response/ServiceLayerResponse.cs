using Auth.Models.Response;

namespace Auth.Domain.Models.Api.Response;

public class ServiceLayerResponse<T> : ApiResponse
{
    public bool IsSuccess { get; private set; } = false;
    public T? Data { get; private set; }
    
    public string? ErrorMessage { get; private set; }

    public static ServiceLayerResponse<ServiceLayerSuccess<T>> Success(ServiceLayerSuccess<T> data)
    {
        return new ServiceLayerResponse<ServiceLayerSuccess<T>>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static ServiceLayerResponse<ServiceLayerError> Fail( ServiceLayerError data, string? errorMessage = null)
    {
        return new ServiceLayerResponse<ServiceLayerError>
        {
            Data = data,
            ErrorMessage = errorMessage
        };
    }
}