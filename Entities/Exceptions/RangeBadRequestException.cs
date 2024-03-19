namespace Entities.Exceptions;

public class RangeBadRequestException:BadRequestException
{
    public RangeBadRequestException(string message) : base(message)
    {
    }
}