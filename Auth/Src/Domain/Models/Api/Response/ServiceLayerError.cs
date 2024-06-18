namespace Auth.Models.Response;

public class ServiceLayerError
{
    public Error Error { get; private set; }
}
public class Error
{
    public int Code { get; set; }
    public Message Message { get; set; }
}

public class Message
{
    public string Lang { get; set; }
    public string Value { get; set; }
}
