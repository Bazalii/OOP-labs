using System.Collections.Generic;
using System.Linq;
using Shops.Services;

namespace Shops.Entities
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

        public void DeliveryOfProducts(ShopManager shopManager, List<Box> delivery)
        {
            foreach (Box box in delivery)
            {
                Box wantedBox = BoxWithProduct(box.ProductId);
                if (wantedBox == null)
                {
                    Boxes.Add(box);
                }
                else
                {
                    wantedBox.ProductPrice = box.ProductPrice;
                    wantedBox.Quantity += box.Quantity;
                }

                Product product = shopManager.Products.Find(product => product.Id == box.ProductId);
                product.TotalQuantity += box.Quantity;
            }
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