using System.Collections.Generic;
using Banks.BanksStructure.Implementations;

namespace Banks.BanksStructure
{
    public abstract class BankPrototype
    {
        public IReadOnlyList<Account> ReadableAccounts => Accounts;

        protected int Id { get; set; }

        protected string Name { get; set; }

        protected int AccountIds { get; set; }

        protected IPercentCalculator PercentCalculator { get; set; }

        protected int AccountsTerm { get; set; }

        protected float LimitIfDoubtful { get; set; }

        protected List<Account> Accounts { get; set; } = new ();

        protected List<Client> Clients { get; set; } = new ();

        public abstract void AddAccount(Account account);

        public abstract void RemoveAccount(Account account);

        public abstract void CloseAccount(Account account);

        public abstract void CreateDepositAccount(Client client, float amountOfMoney);

        public abstract void CloseDepositAccount(Account account);

        public abstract void CreateDebitAccount(Client client, float amountOfMoney);

        public abstract void CreateCreditAccount(Client client, float amountOfMoney);

        public abstract void RegisterAccountAndClient(Account account, Client client);

        public abstract void RegisterClient(Client client);

        public abstract bool CheckIfClientRegistered(Client client);

        public abstract Client GetClientByAccount(Account account);

        public abstract bool GetClientDoubtfulness(Client client);

        public abstract bool CheckIfMonthPassed(Account account);

        public abstract void AddDailyIncome();

        public abstract void ReduceDaysLeft();

        public void SetId(int id)
        {
            Id = id;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }
    }
}