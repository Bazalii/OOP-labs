using System;

namespace Banks.BanksStructure.Implementations
{
    public class ReplenishmentTransaction : Transaction
    {
        public ReplenishmentTransaction(int id, Account accountToReplenish, float amountOfMoney)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id should be a positive integer!", nameof(id));
            }

            Id = id;
            AccountToReplenish = accountToReplenish ??
                                 throw new ArgumentNullException(
                                     nameof(accountToReplenish), "Account to replenish cannot be null!");
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
        }

        public Account AccountToReplenish { get; }
    }
}