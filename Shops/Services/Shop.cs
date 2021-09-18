using System;
using System.Collections.Generic;

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

        public void DeliveryOfProducts(List<Tuple<string, int, int>> delivery)
        {
            foreach (Tuple<string, int, int> tuple in delivery)
            {
                Box wantedBox = ContainsProduct(tuple.Item1);
                if (wantedBox == null)
                {
                    Boxes.Add(new Box(tuple.Item1, tuple.Item2, tuple.Item3));
                }
                else
                {
                    wantedBox.Quantity += tuple.Item3;
                }
            }
        }

        private Box ContainsProduct(string productName)
        {
            foreach (var box in Boxes)
            {
                if (box.ProductName == productName)
                {
                    return box;
                }
            }

            return null;
        }
    }
}