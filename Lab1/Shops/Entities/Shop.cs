using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ProductInShop> _productsInShop;

    public Shop(string name, string address)
    {
        Name = name;
        Address = address;
        Id = Guid.NewGuid();
        _productsInShop = new List<ProductInShop>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Address { get; }

    public IReadOnlyCollection<ProductInShop> ProductsInShop => _productsInShop;

    public ProductInShop? FindProduct(Product product)
    {
        return _productsInShop.Find(x => x.Name == product.Name);
    }

    public void AddProduct(Product product, int amount)
    {
        var productInShop = FindProduct(product);
        if (productInShop == null)
        {
            _productsInShop.Add(new ProductInShop(product.Name, product.Price, amount));
        }
        else
        {
            productInShop.Quantity += amount;
        }
    }

    public void ChangeProductPrice(Product product, decimal newPrice)
    {
        var productInShop = FindProduct(product);
        if (productInShop == null)
        {
            throw new ProductNotFoundException(product.Name);
        }

        productInShop.Price = newPrice;
    }
}