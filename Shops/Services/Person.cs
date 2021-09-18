namespace Shops.Services
{
    public class Person
    {
        public Person(string name, int money)
        {
            Name = name;
            Money = money;
        }

        public string Name { get; private set; }

        public int Money { get; set; }
    }
}