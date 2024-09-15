namespace Catalog.API.Exceptions;
[Serializable]
internal class ProductNotFoundException : NotFoundException
{

    public ProductNotFoundException(Guid Id) : base("Category", Id)
    {
    }
    public ProductNotFoundException(string? message) : base(message)
    {
    }

    public ProductNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}