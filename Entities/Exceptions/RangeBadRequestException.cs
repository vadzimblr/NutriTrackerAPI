namespace Entities.Exceptions;

public class RangeBadRequestException:Exception
{
    public RangeBadRequestException(string message) : base(message)
    {
    }
}