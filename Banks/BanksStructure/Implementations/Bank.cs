using System;
using System.Linq;

namespace Banks.BanksStructure.Implementations
{
    public class Bank : BankPrototype
    {
        public Bank(string name, IPercentCalculator percentCalculator, int accountsTerm, float limitIfDoubtful)
        {
            Name = name ??
                   throw new ArgumentNullException(
                       nameof(name), "Name cannot be null!");
            PercentCalculator = percentCalculator ??
                                throw new ArgumentNullException(
                                    nameof(percentCalculator), "Percent calculator cannot be null!");
            if (accountsTerm <= 0)
            {
                throw new ArgumentException("Term of account should be a positive integer!", nameof(accountsTerm));
            }

            AccountsTerm = accountsTerm;
            if (limitIfDoubtful <= 0)
            {
                throw new ArgumentException("Limit for account should be a positive float!", nameof(limitIfDoubtful));
            }

            LimitIfDoubtful = limitIfDoubtful;
        }

        public override void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public override void RemoveAccount(Account account)
        {
            GetClientByAccount(account).RemoveAccount(account);
            Accounts.Remove(account);
        }

        public override void CloseAccount(Account account)
        {
            RemoveAccount(account);
        }

        public override void CreateDepositAccount(Client client, float amountOfMoney)
        {
            var account = new DepositAccount(
                $"{Id}" + "_" + $"{AccountIds += 1}",
                AccountsTerm,
                PercentCalculator.CalculateDepositPercent(amountOfMoney),
                amountOfMoney,
                GetClientDoubtfulness(client),
                LimitIfDoubtful);
            RegisterAccountAndClient(account, client);
        }

        public override void CloseDepositAccount(Account account)
        {
            CreateDebitAccount(GetClientByAccount(account), account.GetAmountOfMoney());
            RemoveAccount(account);
        }

        public override void CreateDebitAccount(Client client, float amountOfMoney)
        {
            var account = new DebitAccount(
                $"{Id}" + "_" + $"{AccountIds += 1}",
                AccountsTerm,
                PercentCalculator.CalculateDebitPercent(amountOfMoney),
                amountOfMoney,
                GetClientDoubtfulness(client),
                LimitIfDoubtful);
            RegisterAccountAndClient(account, client);
        }

        public override void CreateCreditAccount(Client client, float amountOfMoney)
        {
            var account = new CreditAccount(
                $"{Id}" + "_" + $"{AccountIds += 1}",
                AccountsTerm,
                PercentCalculator.CalculateCreditCommission(amountOfMoney),
                amountOfMoney,
                GetClientDoubtfulness(client),
                LimitIfDoubtful);
            RegisterAccountAndClient(account, client);
        }

        public override void RegisterAccountAndClient(Account account, Client client)
        {
            Accounts.Add(account);
            client.AddAccount(account);
            if (!CheckIfClientRegistered(client))
            {
                RegisterClient(client);
            }
        }

        public override void RegisterClient(Client client)
        {
            Clients.Add(client);
        }

        public override bool CheckIfClientRegistered(Client client)
        {
            return Clients.Contains(client);
        }

        public override Client GetClientByAccount(Account account)
        {
            return Clients.FirstOrDefault(client => client.ReadableAccounts.Contains(account));
        }

        public override bool GetClientDoubtfulness(Client client)
        {
            return client.GetAddress() == null || client.GetPassportNumber() == null;
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
                ReduceDaysLeft();
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

        public override bool Equals(object obj)
        {
            return obj is Bank bank && Equals(bank);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private bool Equals(Bank other)
        {
            return Id == other.Id;
        }
    }
}