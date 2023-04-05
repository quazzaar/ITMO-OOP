using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class MarketplaceService : IMarketplaceService
{
    private readonly List<Shop> _shops = new ();
    private readonly List<Buyer> _buyers = new ();

    public Shop CreateShop(string name, string address)
    {
        var shop = new Shop(name, address);
        _shops.Add(shop);
        return shop;
    }

    public Buyer CreateBuyer(string name, string surname, decimal balance)
    {
        var buyer = new Buyer(balance, name, surname, new List<ProductInShop>());
        _buyers.Add(buyer);
        return buyer;
    }

    public void SupplyProductToShop(Shop shop, Product product, int amount)
    {
        shop.AddProduct(product, amount);
    }

    public void ChangeProductPrice(Shop shop, Product product, decimal newPrice)
    {
        shop.ChangeProductPrice(product, newPrice);
    }

    public void TheCheapestWayToBuyProduct(Buyer buyer, Product product, int amount)
    {
        var shops = _shops
            .Where(x => x.ProductsInShop.Any(y => y.Name == product.Name))
            .OrderBy(x => x.ProductsInShop.First(y => y.Name == product.Name).Price)
            .ToList();
        var productInShop = shops.First().ProductsInShop.First(x => x.Name == product.Name);
        buyer.AddProductToCart(shops.First(), buyer, productInShop, amount);
        buyer.BuyProductsFromCart(buyer, shops.First());
    }

    public void AddProductToCart(Shop shop, Buyer buyer, Product product, int amount)
    {
        buyer.AddProductToCart(shop, buyer, product, amount);
    }

    public void BuyProductsFromCart(Buyer buyer, Shop shop)
    {
        buyer.BuyProductsFromCart(buyer, shop);
    }
}