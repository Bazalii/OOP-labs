using System;
using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, int money)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (money < 0) throw new ArgumentException("Money of person cannot be negative!");
            Money = money;
        }

        public string Name { get; }

        public int Money { get; set; }
    }
}