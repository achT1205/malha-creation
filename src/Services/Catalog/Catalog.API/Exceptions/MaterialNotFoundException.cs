namespace Catalog.API.Exceptions;

[Serializable]
internal class MaterialNotFoundException : NotFoundException
{

    public MaterialNotFoundException(Guid Id) : base("Material", Id)
    {
    }
    public MaterialNotFoundException(string? message) : base(message)
    {
    }

    public MaterialNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

