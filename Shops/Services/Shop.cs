using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        private string _address;
        public Shop(int id, string name, string address)
        {
            Id = id;
            Name = name;
            _address = address;
        }

        public int Id { get; }

        public string Name { get; }

        public int Proceeds { get; set; }

        public List<Box> Boxes { get; } = new ();

        public string DeliveryOfProducts(ShopManager shopManager, List<Tuple<string, int, int>> delivery)
        {
            var notAddedProducts = new List<string>();
            string reason = string.Empty;
            foreach ((string productName, int productPrice, int quantity) in delivery)
            {
                try
                {
                    int productId = shopManager.GetProductId(productName);
                    Box wantedBox = BoxWithProduct(productId);
                    if (wantedBox == null)
                    {
                        Boxes.Add(new Box(productId, productPrice, quantity));
                    }
                    else
                    {
                        wantedBox.ProductPrice = productPrice;
                        wantedBox.Quantity += quantity;
                    }

                    Product product = shopManager.Products.Find(product => product.Id == productId);
                    product.TotalQuantity += quantity;
                }
                catch (NotInBaseException e)
                {
                    notAddedProducts.Add(productName);
                    reason = e.Message;
                }
            }

            if (!notAddedProducts.Any()) return "Delivery is successful!";
            string notAddedProductsString =
                notAddedProducts.Aggregate(string.Empty, (current, product) => current + (product + ", "));

            notAddedProductsString = notAddedProductsString.Remove(notAddedProductsString.Length - 1);
            return $"These products weren't added: {notAddedProductsString} because {reason.ToLower()}";
        }

        public void ChangeProductPrice(ShopManager shopManager, string productName, int newPrice)
        {
            int productId = shopManager.GetProductId(productName);
            Box wantedBox = Boxes.Find(box => box.ProductId == productId);
            wantedBox.ProductPrice = newPrice;
        }

        private Box BoxWithProduct(int productId)
        {
            return Boxes.FirstOrDefault(box => box.ProductId == productId);
        }
    }
}