using System.Net.Http.Headers;
using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IMarketplaceService
{
    Shop CreateShop(string name, string address);
    Buyer CreateBuyer(string name, string surname, decimal balance);
    void SupplyProductToShop(Shop shop, Product product, int amount);
    void ChangeProductPrice(Shop shop, Product product, decimal newPrice);
    void TheCheapestWayToBuyProduct(Buyer buyer, Product product, int amount);
    void AddProductToCart(Shop shop, Buyer buyer, Product product, int amount);
    void BuyProductsFromCart(Buyer buyer, Shop shop);
}