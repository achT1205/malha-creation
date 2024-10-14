namespace Catalog.API.Exceptions;

[Serializable]
internal class CategoryNotFoundException : NotFoundException
{

    public CategoryNotFoundException(Guid Id) : base("Category", Id)
    {
    }
    public CategoryNotFoundException(string? message) : base(message)
    {
    }

    public CategoryNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}