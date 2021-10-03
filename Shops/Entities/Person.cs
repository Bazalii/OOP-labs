namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, int money)
        {
            Name = name;
            Money = money;
        }

        public string Name { get; }

        public int Money { get; set; }
    }
}