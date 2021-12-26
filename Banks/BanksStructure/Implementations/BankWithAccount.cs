using System;

namespace Banks.BanksStructure.Implementations
{
    public class BankWithAccount
    {
        public BankWithAccount(Bank bank, Account account)
        {
            FoundBank = bank ??
                        throw new ArgumentNullException(
                            nameof(bank), "Bank cannot be null!");
            FoundAccount = account ??
                           throw new ArgumentNullException(
                               nameof(account), "Account cannot be null!");
        }

        public Bank FoundBank { get; }

        public Account FoundAccount { get; }
    }
}