using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Entities
{
    public class Shop
    {
        public Shop(int id, string name, string address)
        {
            if (id < 0) throw new ArgumentException("Id of shop cannot be negative!", nameof(id));
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
            Address = address ?? throw new ArgumentNullException(nameof(address), "Name cannot be null");
        }

        public int Id { get; }

        public string Name { get; }

        public string Address { get; }

        public int Proceeds { get; set; }

        public List<Box> Boxes { get; } = new ();

        public void DeliverProducts(List<Box> delivery)
        {
            foreach (Box box in delivery)
            {
                Box wantedBox = GetBoxWithProduct(box.Product);
                if (wantedBox == null)
                {
                    Boxes.Add(box);
                }
                else
                {
                    wantedBox.ProductPrice = box.ProductPrice;
                    wantedBox.Quantity += box.Quantity;
                }

                box.Product.TotalQuantity += box.Quantity;
            }
        }

        public void ChangeProductPrice(Product product, int newPrice)
        {
            Box wantedBox = Boxes.Find(box => box.Product.Id == product.Id);
            wantedBox.ProductPrice = newPrice;
        }

        private Box GetBoxWithProduct(Product product)
        {
            return Boxes.FirstOrDefault(box => box.Product.Id == product.Id);
        }
    }
}