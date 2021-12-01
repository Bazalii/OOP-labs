using System.Collections.Generic;
using System.Linq;
using Banks.Tools;

namespace Banks.BanksStructure.Implementations
{
    public class CentralBank
    {
        private readonly List<Bank> _banks = new ();

        private readonly List<Transaction> _transactions = new ();

        private int _transactionIds;

        private int _bankIds;

        public void RegisterBank(Bank bank)
        {
            bank.SetId(_bankIds += 1);
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
            RegisterTransaction(new ReplenishmentTransaction(_transactionIds += 1, accountToReplenish, amountOfMoney));
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
            RegisterTransaction(new WithdrawalTransaction(_transactionIds += 1, accountToWithdraw, amountOfMoney));
        }

        public void TransferMoney(Account accountToWithdraw, Account accountToReplenish, float amountOfMoney)
        {
            if (accountToWithdraw.Equals(accountToReplenish))
            {
                throw new TheSameAccountsException("Account to withdraw and account to replenish should be different!");
            }

            if (accountToWithdraw.GetDoubtfulness() && amountOfMoney > accountToWithdraw.GetLimitIfIsDoubtful())
            {
                throw new DoubtfulAccountException(
                    $"Your account with id: {accountToWithdraw.GetId()} is doubtful." +
                    $" Please add passport number or address to your account or transfer not more than {accountToWithdraw.GetLimitIfIsDoubtful()}");
            }

            accountToWithdraw.WithdrawMoney(amountOfMoney);
            accountToReplenish.AddMoney(amountOfMoney);
            RegisterTransaction(new TransferTransaction(_transactionIds += 1, accountToWithdraw, accountToReplenish, amountOfMoney));
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

        public List<Transaction> GetClientTransactions(Client client)
        {
            List<Transaction> output = new ();
            foreach (Transaction transaction in _transactions)
            {
                foreach (Account account in client.ReadableAccounts)
                {
                    switch (transaction)
                    {
                        case WithdrawalTransaction withdrawalTransaction:
                            if (account.Equals(withdrawalTransaction.AccountToWithdraw))
                            {
                                output.Add(withdrawalTransaction);
                            }

                            break;
                        case ReplenishmentTransaction replenishmentTransaction:
                            if (account.Equals(replenishmentTransaction.AccountToReplenish))
                            {
                                output.Add(replenishmentTransaction);
                            }

                            break;
                        case TransferTransaction transferTransaction:
                            if (account.Equals(transferTransaction.AccountToWithdraw) ||
                                account.Equals(transferTransaction.AccountToReplenish))
                            {
                                output.Add(transferTransaction);
                            }

                            break;
                    }
                }
            }

            return output;
        }

        public Bank GetBankByName(string name)
        {
            return _banks.FirstOrDefault(bank => bank.GetName() == name);
        }

        public Bank GetBankById(int id)
        {
            return _banks.FirstOrDefault(bank => bank.GetId() == id);
        }

        public List<string> GetAllBankNames()
        {
            return _banks.Select(bank => bank.GetName()).ToList();
        }

        public BankWithAccount GetBankAndAccountByAccountId(string accountId)
        {
            foreach (Bank bank in _banks)
            {
                foreach (Account account in bank.ReadableAccounts)
                {
                    if (account.GetId() == accountId)
                    {
                        return new BankWithAccount(bank, account);
                    }
                }
            }

            throw new NotFoundException($"Account with Id: {accountId} doesn't exist");
        }
    }
}