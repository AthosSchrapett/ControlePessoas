using System.Text.Json;

namespace ControlePessoas.API.Middlewares.Uteis;

public class ErrorResponse(int statusCode, string message)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
