using System.Collections.Generic;
using System.Linq;
using Banks.Tools;

namespace Banks.BanksStructure.Implementations
{
    public class CentralBank
    {
        private int _transactionIds;

        private List<Bank> _banks = new ();

        private List<Transaction> _transactions = new ();

        public void RegisterBank(Bank bank)
        {
            _banks.Add(bank);
        }

        public void AddDailyIncome()
        {
            foreach (Bank bank in _banks)
            {
                bank.AddDailyIncome();
            }
        }

        public void RegisterTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void AddMoney(Account accountToReplenish, float amountOfMoney)
        {
            accountToReplenish.AddMoney(amountOfMoney);
            _transactions.Add(new ReplenishmentTransaction(_transactionIds += 1, accountToReplenish, amountOfMoney));
        }

        public void WithdrawMoney(Account accountToWithdraw, float amountOfMoney)
        {
            if (accountToWithdraw.GetDoubtfulness() && amountOfMoney > accountToWithdraw.GetLimitIfIsDoubtful())
            {
                throw new DoubtfulAccountException(
                    $"Your account with id: {accountToWithdraw.GetId()} is doubtful." +
                    $" Please add passport number or address to your account or withdraw not more than {accountToWithdraw.GetLimitIfIsDoubtful()}");
            }

            accountToWithdraw.WithdrawMoney(amountOfMoney);
            _transactions.Add(new WithdrawalTransaction(_transactionIds += 1, accountToWithdraw, amountOfMoney));
        }

        public void TransferMoney(Account accountToWithdraw, Account accountToReplenish, float amountOfMoney)
        {
            if (accountToWithdraw.GetDoubtfulness() && amountOfMoney > accountToWithdraw.GetLimitIfIsDoubtful())
            {
                throw new DoubtfulAccountException(
                    $"Your account with id: {accountToWithdraw.GetId()} is doubtful." +
                    $" Please add passport number or address to your account or transfer not more than {accountToWithdraw.GetLimitIfIsDoubtful()}");
            }

            accountToWithdraw.WithdrawMoney(amountOfMoney);
            accountToReplenish.AddMoney(amountOfMoney);
            _transactions.Add(new TransferTransaction(_transactionIds += 1, accountToWithdraw, accountToReplenish, amountOfMoney));
        }

        public void CancelTransaction(Transaction transaction)
        {
            float amountOfMoney = transaction.GetAmountOfMoney();
            switch (transaction)
            {
                case WithdrawalTransaction withdrawalTransaction:
                    withdrawalTransaction.AccountToWithdraw.AddMoney(amountOfMoney);
                    break;
                case ReplenishmentTransaction replenishmentTransaction:
                    replenishmentTransaction.AccountToReplenish.WithdrawMoney(amountOfMoney);
                    break;
                case TransferTransaction transferTransaction:
                    transferTransaction.AccountToWithdraw.AddMoney(amountOfMoney);
                    transferTransaction.AccountToReplenish.WithdrawMoney(amountOfMoney);
                    break;
            }

            _transactions.Remove(transaction);
        }

        public Transaction GetTransaction(int id)
        {
            return _transactions.FirstOrDefault(transaction => transaction.GetId() == id);
        }
    }
}