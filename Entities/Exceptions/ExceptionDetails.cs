using System.Text.Json;

namespace Entities.Exceptions;

public class ExceptionDetails
{
    public string StatusCode { get; set; }
    public string? Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}