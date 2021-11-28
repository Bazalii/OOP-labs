using System.Collections.Generic;

namespace Banks.BanksStructure.Implementations
{
    public class Bank : BankPrototype
    {
        public Bank(string name, IPercentCalculator percentCalculator, int accountsTerm, float limitIfDoubtful)
        {
            Name = name;
            PercentCalculator = percentCalculator;
            AccountsTerm = accountsTerm;
            LimitIfDoubtful = limitIfDoubtful;
        }

        public override void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public override void CreateDepositAccount(Client client, float amountOfMoney)
        {
            var account = new DepositAccount(
                AccountIds += 1,
                AccountsTerm,
                PercentCalculator.CalculateDepositPercent(amountOfMoney),
                amountOfMoney,
                GetClientDoubtfulness(client),
                LimitIfDoubtful);
            Accounts.Add(account);
        }

        public override void CreateDebitAccount(Client client, float amountOfMoney)
        {
            var account = new DebitAccount(
                AccountIds += 1,
                AccountsTerm,
                PercentCalculator.CalculateDebitPercent(amountOfMoney),
                amountOfMoney,
                GetClientDoubtfulness(client),
                LimitIfDoubtful);
            Accounts.Add(account);
        }

        public override void CreateCreditAccount(Client client, float amountOfMoney)
        {
            var account = new CreditAccount(
                AccountIds += 1,
                AccountsTerm,
                PercentCalculator.CalculateCreditCommission(amountOfMoney),
                amountOfMoney,
                GetClientDoubtfulness(client),
                LimitIfDoubtful);
            Accounts.Add(account);
        }

        public override void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public override bool GetClientDoubtfulness(Client client)
        {
            return client.GetAddress() == null || client.GetPassportNumber() == 0;
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

        public override void ReduceDaysLeft()
        {
            foreach (Account account in Accounts)
            {
                account.ReduceDaysLeft();
            }
        }
    }
}