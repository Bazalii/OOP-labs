namespace Banks.BanksStructure.Implementations
{
    public class BankWithAccount
    {
        public BankWithAccount(Bank bank, Account account)
        {
            FoundBank = bank;
            FoundAccount = account;
        }

        public Bank FoundBank { get; }

        public Account FoundAccount { get; }
    }
}