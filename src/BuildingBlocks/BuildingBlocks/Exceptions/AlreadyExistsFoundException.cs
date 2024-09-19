namespace BuildingBlocks.Exceptions;

public class AlreadyExistsFoundException : Exception
{
    public AlreadyExistsFoundException(string? message) : base(message)
    {
    }

    public AlreadyExistsFoundException(string name, object key) : base($"Entity \"{name}\" ({key})already exists.")
    {
    }
}