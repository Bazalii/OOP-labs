namespace Banks.ConsoleInterfaceStructure
{
    public class DataForTwoWaysTransactions
    {
        public DataForTwoWaysTransactions(string firstAccountId, string secondAccountId, float amountOfMoney)
        {
            FirstAccountId = firstAccountId;
            SecondAccountId = secondAccountId;
            AmountOfMoney = amountOfMoney;
        }

        public string FirstAccountId { get; }

        public string SecondAccountId { get; }

        public float AmountOfMoney { get; }
    }
}