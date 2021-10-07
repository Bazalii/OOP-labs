using System;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, int money)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
            if (money < 0) throw new ArgumentException("Money of person cannot be negative!", nameof(money));
            Money = money;
        }

        public string Name { get; }

        public int Money { get; set; }
    }
}