using System.Collections.Generic;
using Banks.BanksStructure.Implementations;

namespace Banks.BanksStructure
{
    public abstract class BankPrototype
    {
        protected List<Account> Accounts { get; set; } = new ();

        protected List<Client> Clients { get; set; } = new ();

        public abstract void AddAccount(Account account);

        public abstract void AddClient(Client client);

        public abstract bool CheckIfMonthPassed(Account account);

        public abstract void AddDailyIncome();
    }
}