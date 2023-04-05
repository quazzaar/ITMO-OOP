using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(decimal balance)
        : base($"not enough money {balance}") { }
}