namespace FoodOrder.Endpoint.Helpers;

public class ErrorModel
{
    public string Message { get; }

    public ErrorModel(string message)
    {
        Message = message;
    }
}