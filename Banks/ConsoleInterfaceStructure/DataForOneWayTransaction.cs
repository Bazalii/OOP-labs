using System;

namespace Banks.ConsoleInterfaceStructure
{
    public class DataForOneWayTransaction
    {
        public DataForOneWayTransaction(string accountId, float amountOfMoney)
        {
            AccountId = accountId ??
                        throw new ArgumentNullException(
                            nameof(accountId), "Account id cannot be null!");
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
        }

        public string AccountId { get; }

        public float AmountOfMoney { get; }
    }
}