using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public void Buy(Person person, string productName, int wantedQuantity)
        {
            if (EnoughProduct(productName, wantedQuantity))
            {
                int currentQuantity = 0;
                int price = 0;
                while (currentQuantity != wantedQuantity)
                {
                    int minimalPrice = int.MaxValue;
                    int cheapestProductQuantity = 0;
                    foreach (Shop shop in Shops)
                    {
                        foreach (Box box in shop.Boxes)
                        {
                            if (box.ProductName != productName) continue;
                            if (box.ProductPrice < minimalPrice)
                            {
                                minimalPrice = box.ProductPrice;
                                cheapestProductQuantity = box.Quantity;
                                shop.Boxes.Remove(box);
                            }
                        }
                    }

                    if (currentQuantity + cheapestProductQuantity >= wantedQuantity)
                    {
                        currentQuantity = wantedQuantity;
                        price += (wantedQuantity - currentQuantity) * minimalPrice;
                    }
                    else
                    {
                        currentQuantity += cheapestProductQuantity;
                        price += cheapestProductQuantity * currentQuantity;
                    }
                }

                if (person.Money - price < 0)
                {
                    throw new NotEnoughMoneyException(
                        $"You don't have enough money to buy {wantedQuantity} pieces of {productName}");
                }

                person.Money -= price;
            }
        }

        public bool EnoughProduct(string productName, int wantedQuantity)
        {
            foreach (Product product in Products)
            {
                if (product.Name == productName)
                {
                    if (product.TotalQuantity >= wantedQuantity)
                    {
                        return true;
                    }

                    throw new NotEnoughProductException($"$You can buy only {product.TotalQuantity} pieces of this product");
                }
            }

            return false;
        }
    }
}