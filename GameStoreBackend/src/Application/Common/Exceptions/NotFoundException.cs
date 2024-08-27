namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(): base()
    {}

    public NotFoundException(string message) : base(message)
    {}
    public NotFoundException(string name, Object key) : base($"{name} with id {key} was not found")
    {}
}