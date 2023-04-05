using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ProductInShop : Product
{
    public ProductInShop(string name, decimal price, int quantity)
        : base(name, price)
    {
        Quantity = quantity;
    }

    public int Quantity { get; set; }
}
