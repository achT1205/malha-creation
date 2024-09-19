namespace Catalog.API.Exceptions;

[Serializable]
internal class ProductAlreadyExistsFoundException : AlreadyExistsFoundException
{

    public ProductAlreadyExistsFoundException(Guid Id) : base("Product", Id)
    {
    }
    public ProductAlreadyExistsFoundException(string? message) : base(message)
    {
    }

    public ProductAlreadyExistsFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}