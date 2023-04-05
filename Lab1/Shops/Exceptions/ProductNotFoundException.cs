namespace Shops.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string name)
        : base($"Product {name} does not exist") { }
}