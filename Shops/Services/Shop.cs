using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        public Shop(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public int Proceeds { get; set; } = 0;

        public List<Box> Boxes { get; private set; } = new List<Box>();

        public void DeliveryOfProducts(ShopManger shopManager, List<Tuple<string, int, int>> delivery)
        {
            List<string> notAddedProducts = new List<string>();
            string reason = string.Empty;
            foreach (Tuple<string, int, int> tuple in delivery)
            {
                try
                {
                    Box wantedBox = BoxWithProduct(shopManager.GetProductId(tuple.Item1));
                    if (wantedBox == null)
                    {
                        Boxes.Add(new Box(shopManager.GetProductId(tuple.Item1), tuple.Item2, tuple.Item3));
                    }
                    else
                    {
                        wantedBox.Quantity += tuple.Item3;
                    }
                }
                catch (NotInBaseException e)
                {
                    notAddedProducts.Add(tuple.Item1);
                    reason = e.Message;
                }

                if (notAddedProducts.Any())
                {
                    Console.WriteLine($"These products weren't added: {notAddedProducts}, because {reason}");
                }
            }
        }

        private Box BoxWithProduct(int productId)
        {
            foreach (var box in Boxes)
            {
                if (box.ProductId == productId)
                {
                    return box;
                }
            }

            return null;
        }
    }
}