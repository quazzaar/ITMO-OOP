using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class MarketplaceServiceTests
{
    [Fact]
    public void SupplyProductsToShop_ProductsAreReadyToBuy()
    {
        var newMarketplace = new MarketplaceService();
        var newCarShop = newMarketplace.CreateShop("Porsche", "Saint-Petersburg");
        var taycan = new Product("Taycan", 20000);
        newMarketplace.SupplyProductToShop(newCarShop, taycan, 40);
        Assert.Equal(40, newCarShop.ProductsInShop.First(x => x.Name == "Taycan").Quantity);
    }

    [Fact]
    public void ChangePriceOfProduct_PriceChanged()
    {
        var newMarketplace = new MarketplaceService();
        var newCarShop = newMarketplace.CreateShop("Porsche", "Saint-Petersburg");
        var taycan = new Product("Taycan", 20000);
        newMarketplace.SupplyProductToShop(newCarShop, taycan, 40);
        newMarketplace.ChangeProductPrice(newCarShop, taycan, 30000);
        Assert.Equal(30000, newCarShop.ProductsInShop.First(x => x.Name == "Taycan").Price);
    }

    [Fact]
    public void FindCheapestShopToBuyProduct()
    {
        var newMarketplace = new MarketplaceService();
        var newCarShop = newMarketplace.CreateShop("Porsche", "Saint-Petersburg");
        var newCarShop2 = newMarketplace.CreateShop("Porsche", "Moscow");
        var newBuyer = newMarketplace.CreateBuyer("Dimas", "Dimas", 1000000);
        var taycan = new Product("Taycan", 20000);
        newMarketplace.SupplyProductToShop(newCarShop, taycan, 40);
        newMarketplace.SupplyProductToShop(newCarShop2, taycan, 40);
        newMarketplace.ChangeProductPrice(newCarShop, taycan, 30000);
        newMarketplace.TheCheapestWayToBuyProduct(newBuyer, taycan, 2);
        Assert.Equal(960000, newBuyer.Balance);
    }

    [Fact]
    public void FindCheapestShopToBuyProduct_ThereAreNotEnoughProducts()
    {
        var newMarketplace = new MarketplaceService();
        var newCarShop = newMarketplace.CreateShop("Porsche", "Saint-Petersburg");
        var newCarShop2 = newMarketplace.CreateShop("Porsche", "Moscow");
        var newBuyer = newMarketplace.CreateBuyer("Dimas", "Dimas", 1000000);
        var taycan = new Product("Taycan", 20000);
        newMarketplace.SupplyProductToShop(newCarShop, taycan, 40);
        newMarketplace.SupplyProductToShop(newCarShop2, taycan, 40);
        newMarketplace.ChangeProductPrice(newCarShop, taycan, 30000);
        Assert.Throws<NotEnoughProductsException>(() =>
            newMarketplace.TheCheapestWayToBuyProduct(newBuyer, taycan, 50));
    }

    [Fact]
    public void FindCheapestShopToBuyProduct_ThereAreNotEnoughMoney()
    {
        var newMarketplace = new MarketplaceService();
        var newCarShop = newMarketplace.CreateShop("Porsche", "Saint-Petersburg");
        var newCarShop2 = newMarketplace.CreateShop("Porsche", "Moscow");
        var newBuyer = newMarketplace.CreateBuyer("Dimas", "Dimas", 10000);
        var taycan = new Product("Taycan", 20000);
        newMarketplace.SupplyProductToShop(newCarShop, taycan, 40);
        newMarketplace.SupplyProductToShop(newCarShop2, taycan, 40);
        newMarketplace.ChangeProductPrice(newCarShop, taycan, 30000);
        Assert.Throws<NotEnoughMoneyException>(() =>
            newMarketplace.TheCheapestWayToBuyProduct(newBuyer, taycan, 1));
    }

    [Fact]
    public void CustomerBuyMultipleAmountOfProducts_CustomerBalanceHasChangedAndAmountOfProductsReduced()
    {
        var newMarketplace = new MarketplaceService();
        var newCarShop = newMarketplace.CreateShop("Porsche", "Saint-Petersburg");
        var newCarShop2 = newMarketplace.CreateShop("Porsche", "Moscow");
        var newBuyer = newMarketplace.CreateBuyer("Dimas", "Dimas", 200000);
        var taycan = new Product("Taycan", 20000);
        var panamera = new Product("Panamera", 10000);
        newMarketplace.SupplyProductToShop(newCarShop, taycan, 40);
        newMarketplace.SupplyProductToShop(newCarShop2, taycan, 40);
        newMarketplace.SupplyProductToShop(newCarShop, panamera, 40);
        newMarketplace.SupplyProductToShop(newCarShop2, panamera, 40);

        newMarketplace.AddProductToCart(newCarShop, newBuyer, taycan, 2);
        newMarketplace.AddProductToCart(newCarShop, newBuyer, panamera, 2);
        newMarketplace.BuyProductsFromCart(newBuyer, newCarShop);
        Assert.Equal(140000, newBuyer.Balance);
        Assert.Equal(38, newCarShop.ProductsInShop.First(x => x.Name == "Taycan").Quantity);
        Assert.Equal(38, newCarShop.ProductsInShop.First(x => x.Name == "Panamera").Quantity);
    }
}