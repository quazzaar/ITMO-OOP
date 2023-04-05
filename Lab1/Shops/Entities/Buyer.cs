using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Buyer
{
    private readonly List<ProductInShop> _productsInCart;
    private decimal _balance;
    public Buyer(decimal balance, string name, string surname, List<ProductInShop> productsInCart)
    {
        _balance = balance;
        Name = name;
        Surname = surname;
        _productsInCart = productsInCart;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public string Surname { get; }
    public Guid Id { get; }
    public decimal Balance => _balance;
    public IReadOnlyCollection<ProductInShop> ProductsInCart => _productsInCart;

    public void AddBalance(decimal amount)
    {
        _balance += amount;
    }

    public void DownBalance(decimal amount)
    {
        _balance -= amount;
    }

    public void AddProductToCart(Shop shop, Buyer buyer, Product product, int amount)
    {
        var productInShop = shop.FindProduct(product);
        if (productInShop == null)
        {
            throw new ProductNotFoundException(product.Name);
        }

        if (productInShop.Quantity < amount)
        {
            throw new NotEnoughProductsException(amount);
        }

        if (buyer.Balance < productInShop.Price * amount)
        {
            throw new NotEnoughMoneyException(buyer.Balance);
        }

        _productsInCart.Add(new ProductInShop(product.Name, product.Price, amount));
    }

    public void BuyProductsFromCart(Buyer buyer, Shop shop)
    {
        foreach (var productInCart in _productsInCart)
        {
            var productInShop = shop.FindProduct(productInCart);
            if (productInShop == null)
            {
                throw new ProductNotFoundException(productInCart.Name);
            }

            if (productInShop.Quantity < productInCart.Quantity)
            {
                throw new NotEnoughProductsException(productInCart.Quantity);
            }

            if (buyer.Balance < productInShop.Price * productInCart.Quantity)
            {
                throw new NotEnoughMoneyException(buyer.Balance);
            }

            productInShop.Quantity -= productInCart.Quantity;
            buyer.DownBalance(productInShop.Price * productInCart.Quantity);
        }
    }
}