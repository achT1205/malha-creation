namespace Catalog.Domain.Exceptions;

public class CatalogDomainException : Exception
{
    public CatalogDomainException(string message)
        : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
    {
    }
}