using System;

namespace Banks.ConsoleInterfaceStructure
{
    public class DataForNewAccount
    {
        public DataForNewAccount(string bankName, string accountType, float amountOfMoney)
        {
            BankName = bankName ??
                       throw new ArgumentNullException(
                           nameof(bankName), "Bank name cannot be null!");
            AccountType = accountType ??
                          throw new ArgumentNullException(
                              nameof(accountType), "Account type cannot be null!");
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
        }

        public string BankName { get; }

        public string AccountType { get; }

        public float AmountOfMoney { get; }
    }
}