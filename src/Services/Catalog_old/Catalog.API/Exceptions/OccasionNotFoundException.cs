namespace Catalog.API.Exceptions;
[Serializable]
internal class OccasionNotFoundException : NotFoundException
{

    public OccasionNotFoundException(Guid Id) : base("Category", Id)
    {
    }
    public OccasionNotFoundException(string? message) : base(message)
    {
    }

    public OccasionNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}