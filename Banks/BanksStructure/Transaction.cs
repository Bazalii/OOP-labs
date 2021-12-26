namespace Banks.BanksStructure
{
    public abstract class Transaction
    {
        protected int Id { get; init; }
        protected float AmountOfMoney { get; set; }

        public int GetId()
        {
            return Id;
        }

        public float GetAmountOfMoney()
        {
            return AmountOfMoney;
        }
    }
}