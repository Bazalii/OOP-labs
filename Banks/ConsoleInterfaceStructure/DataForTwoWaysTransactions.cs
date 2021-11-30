using System;

namespace Banks.ConsoleInterfaceStructure
{
    public class DataForTwoWaysTransactions
    {
        public DataForTwoWaysTransactions(string firstAccountId, string secondAccountId, float amountOfMoney)
        {
            FirstAccountId = firstAccountId ??
                             throw new ArgumentNullException(
                                 nameof(firstAccountId), "Account id cannot be null!");
            SecondAccountId = secondAccountId ??
                              throw new ArgumentNullException(
                                  nameof(secondAccountId), "Account id cannot be null!");
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
        }

        public string FirstAccountId { get; }

        public string SecondAccountId { get; }

        public float AmountOfMoney { get; }
    }
}