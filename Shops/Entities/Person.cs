using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, int money)
        {
            Name = name;
            if (money < 0) throw new NegativeValueException("Money of person cannot be negative!");
            Money = money;
        }

        public string Name { get; }

        public int Money { get; set; }
    }
}