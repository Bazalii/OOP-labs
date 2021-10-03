using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private int _productIds;

        private int _shopIds;

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

        public Product GetProduct(string productName)
        {
            return Products.Find(product => product.Name == productName) ??
                   throw new NotInBaseException("The product that you want to buy doesn't exist");
        }

        /*
         * If there are enough pieces of products according to base,
         * function finds the cheapest set of products that person
         * wants to buy in all existing shops
         */
        public void Buy(Person person, List<ProductOrder> productsToBuy)
        {
            int price = 0;
            var proceeds = new List<Proceed>();
            foreach (ProductOrder order in productsToBuy)
            {
                int wantedQuantity = order.Quantity;
                if (!EnoughProduct(order.Product.Id, wantedQuantity))
                    throw new NotEnoughProductException($"$You can't buy {wantedQuantity} pieces of this product");
                proceeds.AddRange(GetShopProceed(order, ref price));
            }

            PayForOrder(person, price, proceeds);
        }

        private List<Proceed> GetShopProceed(ProductOrder order, ref int price)
        {
            List<Proceed> currentProceeds = new ();
            int wantedQuantity = order.Quantity;
            int currentQuantity = 0;
            Shop shopWithCheapestProduct = Shops[0];
            var boxWithCheapestProduct = new Box();
            int minimalPrice = int.MaxValue;
            while (currentQuantity != wantedQuantity)
            {
                int cheapestProductQuantity = 0;
                GetMinimalProductStatistics(
                    ref minimalPrice,
                    order.Product,
                    ref cheapestProductQuantity,
                    ref shopWithCheapestProduct,
                    ref boxWithCheapestProduct);
                int currentBill = GetBillForBuyingProductsInShop(
                    ref currentQuantity,
                    cheapestProductQuantity,
                    wantedQuantity,
                    minimalPrice,
                    boxWithCheapestProduct);
                price += currentBill;
                currentProceeds.Add(new Proceed(
                    shopWithCheapestProduct,
                    currentBill));
                foreach (Proceed proceed in currentProceeds)
                {
                    Console.WriteLine($"{proceed.ProceedValue}");
                }

                Console.WriteLine("\n");
            }

            return currentProceeds;
        }

        private void GetMinimalProductStatistics(
            ref int minimalPrice,
            Product product,
            ref int cheapestProductQuantity,
            ref Shop shopWithCheapestProduct,
            ref Box boxWithCheapestProduct)
        {
            minimalPrice = int.MaxValue;
            foreach (Shop shop in Shops)
            {
                foreach (Box box in shop.Boxes)
                {
                    if (box.Product.Id != product.Id || box.ProductPrice >= minimalPrice || box.Quantity <= 0) continue;
                    minimalPrice = box.ProductPrice;
                    cheapestProductQuantity = box.Quantity;
                    shopWithCheapestProduct = shop;
                    boxWithCheapestProduct = box;
                }
            }
        }

        private int GetBillForBuyingProductsInShop(
            ref int currentQuantity,
            int cheapestProductQuantity,
            int wantedQuantity,
            int minimalPrice,
            Box boxWithCheapestProduct)
        {
            int currentBill;
            if (currentQuantity + cheapestProductQuantity >= wantedQuantity)
            {
                currentBill = (wantedQuantity - currentQuantity) * minimalPrice;
                boxWithCheapestProduct.Quantity -= wantedQuantity - currentQuantity;
                currentQuantity = wantedQuantity;
            }
            else
            {
                currentQuantity += cheapestProductQuantity;
                currentBill = boxWithCheapestProduct.ProductPrice * cheapestProductQuantity;
                boxWithCheapestProduct.Quantity = 0;
            }

            return currentBill;
        }

        private void PayForOrder(Person person, int price, List<Proceed> proceeds)
        {
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
            return wantedProduct != null && wantedProduct.TotalQuantity > wantedQuantity;
        }
    }
}