using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private int _productIds;

        private int _shopIds;
        public ShopManager()
        {
        }

        public List<Product> Products { get; private set; } = new ();

        private List<Shop> Shops { get; } = new ();

        public void AddNewShop(string shopName, string shopAddress)
        {
            Shops.Add(new Shop(_shopIds += 1, shopName, shopAddress));
        }

        public Shop FindShop(string shopName)
        {
            int shopId = GetShopId(shopName);
            return Shops.FirstOrDefault(shop => shop.Id == shopId);
        }

        public void RegisterNewProduct(string name)
        {
            Products.Add(new Product(_productIds += 1, name));
        }

        public int GetProductId(string productName)
        {
            Product wantedProduct = Products.Find(product => product.Name == productName);
            return wantedProduct?.Id ?? throw new NotInBaseException("The product that you want to buy doesn't exist");
        }

        /*
         * If there are enough pieces of products according to base,
         * function finds the cheapest set of products that person
         * wants to buy in all existing shops
         */
        public void Buy(Person person, List<Order> productsToBuy)
        {
            int price = 0;
            var proceeds = new List<Proceed>();
            foreach (Order order in productsToBuy)
            {
                int productId = GetProductId(order.ProductName);
                int wantedQuantity = order.ProductQuantity;
                if (!EnoughProduct(productId, wantedQuantity)) return;
                int currentQuantity = 0;
                Shop shopWithCheapestProduct = Shops[0];
                var boxWithCheapestProduct = new Box();
                while (currentQuantity != wantedQuantity)
                {
                    int minimalPrice = int.MaxValue;
                    int cheapestProductQuantity = 0;
                    foreach (Shop shop in Shops)
                    {
                        foreach (Box box in shop.Boxes
                            .Where(box => box.ProductId == productId)
                            .Where(box => box.ProductPrice < minimalPrice && box.Quantity > 0))
                        {
                            minimalPrice = box.ProductPrice;
                            cheapestProductQuantity = box.Quantity;
                            shopWithCheapestProduct = shop;
                            boxWithCheapestProduct = box;
                        }
                    }

                    int currentPrice;
                    if (currentQuantity + cheapestProductQuantity >= wantedQuantity)
                    {
                        currentPrice = (wantedQuantity - currentQuantity) * minimalPrice;
                        boxWithCheapestProduct.Quantity -= wantedQuantity - currentQuantity;
                        currentQuantity = wantedQuantity;
                        price += currentPrice;
                        proceeds.Add(new Proceed(
                            shopWithCheapestProduct,
                            currentPrice));
                    }
                    else
                    {
                        currentQuantity += cheapestProductQuantity;
                        currentPrice = boxWithCheapestProduct.ProductPrice * cheapestProductQuantity;
                        price += currentPrice;
                        proceeds.Add(new Proceed(
                            shopWithCheapestProduct,
                            currentPrice));
                        boxWithCheapestProduct.Quantity = 0;
                    }
                }
            }

            if (person.Money - price < 0)
            {
                throw new NotEnoughMoneyException(
                    $"You don't have enough money to buy everything");
            }

            person.Money -= price;
            foreach (Proceed proceed in proceeds)
            {
                proceed.Shop.Proceeds += proceed.ProceedValue;
            }
        }

        private int GetShopId(string shopName)
        {
            Shop wantedShop = Shops.Find(shop => shop.Name == shopName);
            return wantedShop?.Id ?? throw new NotInBaseException("The product that you want to buy doesn't exist");
        }

        private bool EnoughProduct(int productId, int wantedQuantity)
        {
            Product wantedProduct = Products.Find(product => product.Id == productId);
            return wantedProduct == null || (wantedProduct.TotalQuantity <= wantedQuantity)
                ? throw new NotEnoughProductException($"$You can't buy {wantedQuantity} pieces of this product")
                : true;
        }
    }
}