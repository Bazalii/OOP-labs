namespace Banks.ConsoleInterfaceStructure
{
    public class DataForNewAccount
    {
        public DataForNewAccount(string bankName, string accountType, float amountOfMoney)
        {
            BankName = bankName;
            AccountType = accountType;
            AmountOfMoney = amountOfMoney;
        }

        public string BankName { get; }

        public string AccountType { get; }

        public float AmountOfMoney { get; }
    }
}