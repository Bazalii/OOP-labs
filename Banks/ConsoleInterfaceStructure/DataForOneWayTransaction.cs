namespace Banks.ConsoleInterfaceStructure
{
    public class DataForOneWayTransaction
    {
        public DataForOneWayTransaction(string accountId, float amountOfMoney)
        {
            AccountId = accountId;
            AmountOfMoney = amountOfMoney;
        }

        public string AccountId { get; }

        public float AmountOfMoney { get; }
    }
}