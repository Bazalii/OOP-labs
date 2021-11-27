using System.Collections.Generic;

namespace Banks.BanksStructure.Implementations
{
    public class Bank : BankPrototype
    {
        public override void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public override void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public override bool CheckIfMonthPassed(Account account)
        {
            return account.GetTermAndDaysLeftDiff() % 30 == 0;
        }

        public override void AddDailyIncome()
        {
            foreach (Account account in Accounts)
            {
                account.AddDailyIncome();
                if (account is SavingsAccount savingsAccount && CheckIfMonthPassed(account))
                {
                    savingsAccount.AddMonthlyIncome();
                }
            }
        }
    }
}