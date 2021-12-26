using System;

namespace Banks.BanksStructure.Implementations
{
    public class WithdrawalTransaction : Transaction
    {
        public WithdrawalTransaction(int id, Account accountToWithdraw, float amountOfMoney)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id should be a positive integer!", nameof(id));
            }

            Id = id;
            AccountToWithdraw = accountToWithdraw ??
                                throw new ArgumentNullException(
                                    nameof(accountToWithdraw), "Account to withdraw cannot be null!");
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
        }

        public Account AccountToWithdraw { get; }
    }
}