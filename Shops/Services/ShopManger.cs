using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManger
    {
        private int _productIds;

        private int _shopIds;
        public ShopManger()
        {
        }

        public List<Shop> Shops { get; private set; } = new List<Shop>();

        public List<Product> Products { get; private set; } = new List<Product>();

        public void AddNewShop(string name, string address)
        {
            Shops.Add(new Shop(_shopIds += 1, name, address));
        }

        public void RegisterNewProduct(string name)
        {
            Products.Add(new Product(_productIds += 1, name));
        }

        public int GetProductId(string productName)
        {
            foreach (Product product in Products)
            {
                if (product.Name == productName)
                {
                    return product.Id;
                }
            }

            throw new NotInBaseException("The product that you want to buy doesn't exist");
        }

        public void Buy(Person person, string productName, int wantedQuantity)
        {
            int productId = GetProductId(productName);
            if (!EnoughProduct(productId, wantedQuantity)) return;
            int currentQuantity = 0;
            int price = 0;
            Shop shopWithCheapestProduct = Shops[0];
            var boxWithCheapestProduct = new Box();
            var proceeds = new List<Tuple<Shop, int>>();
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
                    currentQuantity = wantedQuantity;
                    currentPrice = (wantedQuantity - currentQuantity) * minimalPrice;
                    price += currentPrice;
                    proceeds.Add(new Tuple<Shop, int>(
                        shopWithCheapestProduct,
                        currentPrice));
                    boxWithCheapestProduct.Quantity -= wantedQuantity - currentQuantity;
                }
                else
                {
                    currentQuantity += cheapestProductQuantity;
                    currentPrice = cheapestProductQuantity * currentQuantity;
                    price += currentPrice;
                    proceeds.Add(new Tuple<Shop, int>(
                        shopWithCheapestProduct,
                        currentPrice));
                    boxWithCheapestProduct.Quantity = 0;
                }
            }

            if (person.Money - price < 0)
            {
                throw new NotEnoughMoneyException(
                    $"You don't have enough money to buy {wantedQuantity} pieces of {productName}");
            }

            person.Money -= price;
            foreach (Tuple<Shop, int> proceed in proceeds)
            {
                proceed.Item1.Proceeds += proceed.Item2;
            }
        }

        private bool EnoughProduct(int productId, int wantedQuantity)
        {
            foreach (Product product in Products.Where(product => product.Id == productId))
            {
                if (product.TotalQuantity >= wantedQuantity)
                {
                    return true;
                }

                throw new NotEnoughProductException($"$You can't buy {wantedQuantity} pieces of this product");
            }

            return false;
        }
    }
}