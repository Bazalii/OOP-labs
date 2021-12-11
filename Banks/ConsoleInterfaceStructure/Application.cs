using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BanksStructure;
using Banks.BanksStructure.Implementations;
using Banks.Tools;

namespace Banks.ConsoleInterfaceStructure
{
    public class Application
    {
        private readonly ConsoleInterface _console = new ();

        private readonly CentralBank _centralBank = new ();

        private readonly IPercentCalculator _newPercentCalculator = new CommonPercentCalculator(
            new List<DepositSumWithPercent>
            {
                new (20000, 2.5F),
                new (30000, 4F),
                new (70000, 5F),
            },
            2F,
            30F);

        private Client _currentClient;

        private List<string> _accountTypes = new () { "Deposit", "Debit", "Credit" };
        public void Process()
        {
            SetUp();
            _console.Start();
            while (true)
            {
                string command = _console.GetCommand();
                switch (command)
                {
                    case "commands":
                        _console.GetAvailableCommands();
                        break;
                    case "getAccounts":
                        _console.GetAccountsStatus(_currentClient.ReadableAccounts);
                        break;
                    case "getTransactions":
                        _console.GetTransactions(_centralBank.GetClientTransactions(_currentClient));
                        break;
                    case "registerClient":
                        RegisterClient(_console.RegisterClient());
                        break;
                    case "registerAccount":
                        RegisterAccount(_console.RegisterAccount(_centralBank.GetAllBankNames(), _accountTypes));
                        break;
                    case "closeAccount":
                        CloseAccount(_console.CloseAccount(_currentClient.GetAccountIds()));
                        break;
                    case "withdraw":
                        WithdrawMoney(_console.WithdrawMoney(_currentClient.GetAccountIds()));
                        break;
                    case "replenish":
                        ReplenishMoney(_console.ReplenishMoney(_currentClient.GetAccountIds()));
                        break;
                    case "transfer":
                        TransferMoney(_console.TransferMoney(_currentClient.GetAccountIds()));
                        break;
                    case "cancel":
                        CancelTransaction(_console.CancelTransaction(_centralBank
                            .GetClientTransactions(_currentClient).Select(transaction => transaction.GetId())
                            .ToList()));
                        break;
                    case "subscribe":
                        Subscribe(_console.Subscribe(_centralBank.GetAllBankNames()));
                        break;
                    case "unsubscribe":
                        Unsubscribe(_console.Unsubscribe(_centralBank.GetAllBankNames()));
                        break;
                    case "changePercents":
                        ChangePercents(_console.ChangePercents(_centralBank.GetAllBankNames()));
                        break;
                    case "scroll":
                        ScrollDays(_console.ScrollDays());
                        break;
                    case "quit":
                        _console.OnEnd();
                        return;
                }
            }
        }

        private void SetUp()
        {
            IPercentCalculator firstPercentCalculator = new CommonPercentCalculator(
                new List<DepositSumWithPercent>()
                {
                    new (50000, 3F),
                    new (100000, 4F),
                    new (150000, 5F),
                },
                2.0F,
                20.0F);
            IPercentCalculator secondPercentCalculator = new CommonPercentCalculator(
                new List<DepositSumWithPercent>()
                {
                    new (30000, 3F),
                    new (50000, 4.5F),
                    new (100000, 5.5F),
                },
                2.5F,
                25.5F);
            Bank firstBank = new ("Sber", firstPercentCalculator, 180, 10000);
            Bank secondBank = new ("Spb", secondPercentCalculator, 180, 10000);
            _centralBank.RegisterBank(firstBank);
            _centralBank.RegisterBank(secondBank);
            Client client = new ("Alexey", "Ivanov", "Nauki prospekt, 30, 110", "1234567");
            secondBank.RegisterClient(client);
            secondBank.CreateCreditAccount(client, 10000);
            secondBank.Subscribe(client);
        }

        private void RegisterClient(Client client)
        {
            _currentClient = client;
        }

        private void RegisterAccount(DataForNewAccount dataForNewAccount)
        {
            Bank bank = _centralBank.GetBankByName(dataForNewAccount.BankName);
            switch (dataForNewAccount.AccountType)
            {
                case "Deposit":
                    bank.CreateDepositAccount(_currentClient, dataForNewAccount.AmountOfMoney);
                    break;
                case "Debit":
                    bank.CreateDebitAccount(_currentClient, dataForNewAccount.AmountOfMoney);
                    break;
                case "Credit":
                    bank.CreateCreditAccount(_currentClient, dataForNewAccount.AmountOfMoney);
                    break;
            }
        }

        private void CloseAccount(string accountId)
        {
            BankWithAccount bankWithAccount = _centralBank.GetBankAndAccountByAccountId(accountId);
            bankWithAccount.FoundBank.CloseAccount(bankWithAccount.FoundAccount);
        }

        private void WithdrawMoney(DataForOneWayTransaction dataForOneWayTransaction)
        {
            BankWithAccount bankWithAccount = _centralBank.GetBankAndAccountByAccountId(dataForOneWayTransaction.AccountId);
            _centralBank.WithdrawMoney(bankWithAccount.FoundAccount, dataForOneWayTransaction.AmountOfMoney);
        }

        private void ReplenishMoney(DataForOneWayTransaction dataForOneWayTransaction)
        {
            BankWithAccount bankWithAccount = _centralBank.GetBankAndAccountByAccountId(dataForOneWayTransaction.AccountId);
            _centralBank.AddMoney(bankWithAccount.FoundAccount, dataForOneWayTransaction.AmountOfMoney);
        }

        private void TransferMoney(DataForTwoWaysTransactions dataForTwoWaysTransactions)
        {
            BankWithAccount bankWithFirstAccount = _centralBank.GetBankAndAccountByAccountId(dataForTwoWaysTransactions.FirstAccountId);
            BankWithAccount bankWithSecondAccount = _centralBank.GetBankAndAccountByAccountId(dataForTwoWaysTransactions.SecondAccountId);
            try
            {
                _centralBank.TransferMoney(
                    bankWithFirstAccount.FoundAccount,
                    bankWithSecondAccount.FoundAccount,
                    dataForTwoWaysTransactions.AmountOfMoney);
            }
            catch (TheSameAccountsException exception)
            {
                _console.ReflectException(exception.Message);
            }
        }

        private void CancelTransaction(int id)
        {
            _centralBank.CancelTransaction(_centralBank.GetTransaction(id));
        }

        private void Subscribe(string bankName)
        {
            _centralBank.GetBankByName(bankName).Subscribe(_currentClient);
        }

        private void Unsubscribe(string bankName)
        {
            _currentClient.Unsubscribe(bankName);
        }

        private void ChangePercents(string bankName)
        {
            Bank bank = _centralBank.GetBankByName(bankName);
            _console.ShowMessages(bank.SetPercentCalculator(_newPercentCalculator));
        }

        private void ScrollDays(int days)
        {
            _centralBank.ScrollDays(days);
        }
    }
}