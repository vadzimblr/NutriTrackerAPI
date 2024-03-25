namespace Entities.Exceptions;

public class LimitNotFoundException:NotFoundException
{
    public LimitNotFoundException() : base("You haven't set the limits yet")
    {
    }
}