namespace Shops.Exceptions;

public class NotEnoughProductsException : Exception
{
    public NotEnoughProductsException(int quantity)
        : base($"{quantity} products left") { }
}