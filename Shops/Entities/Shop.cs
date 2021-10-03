using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        public Shop(int id, string name, string address)
        {
            if (id < 0) throw new ArgumentException("Id of shop cannot be negative!");
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public int Id { get; }

        public string Name { get; }

        public string Address { get; }

        public int Proceeds { get; set; }

        public List<Box> Boxes { get; } = new ();

        public void DeliveryOfProducts(ShopManager shopManager, List<Box> delivery)
        {
            foreach (Box box in delivery)
            {
                Box wantedBox = GetBoxWithProduct(box.Product.Id);
                if (wantedBox == null)
                {
                    Boxes.Add(box);
                }
                else
                {
                    wantedBox.ProductPrice = box.ProductPrice;
                    wantedBox.Quantity += box.Quantity;
                }

                Product product = shopManager.Products.Find(product => product.Id == box.Product.Id);
                product.TotalQuantity += box.Quantity;
            }
        }

        public void ChangeProductPrice(int productId, int newPrice)
        {
            Box wantedBox = Boxes.Find(box => box.Product.Id == productId);
            wantedBox.ProductPrice = newPrice;
        }

        private Box GetBoxWithProduct(int productId)
        {
            return Boxes.FirstOrDefault(box => box.Product.Id == productId);
        }
    }
}