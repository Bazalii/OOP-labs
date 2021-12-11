using System;
using System.Collections.Generic;
using Banks.BanksStructure;
using Banks.BanksStructure.Implementations;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class BanksTest
    {
        private CentralBank _centralBank;

        private IPercentCalculator _percentCalculator;

        private Client _firstClient = new ("Peter", "Pavlov", "Nevskiy pr., 31, 25", "65478953");
        
        private Client _secondClient = new ("Paul", "Jonson", "Ligovskii pr., 110, 37", "65895231");

        private Client _doubtfulClient = new ("Alex", "Jonson");
        
        [SetUp]
        public void SetUp()
        {
            _centralBank = new CentralBank();
            _percentCalculator = new CommonPercentCalculator(
                new List<DepositSumWithPercent>()
                {
                    new (10000, 2F),
                    new (50000, 4F),
                    new (100000, 6F),
                },
                2F,
                30F);
            _centralBank.RegisterBank(new Bank("Sber", _percentCalculator, 180, 10000F));
            _centralBank.RegisterBank(new Bank("Spb", _percentCalculator, 365, 5000F));
            _firstClient = new Client("Peter", "Pavlov", "Nevskiy pr., 31, 25", "65478953");
            _centralBank.GetBankByName("Sber").RegisterClient(_firstClient);
            _centralBank.GetBankByName("Sber").CreateDebitAccount(_firstClient, 100000F);
            _secondClient = new Client("Paul", "Jonson", "Ligovskii pr., 110, 37", "65895231");
            _centralBank.GetBankByName("Spb").RegisterClient(_secondClient);
            _centralBank.GetBankByName("Spb").CreateCreditAccount(_secondClient, 50000F);
        }
        
        [Test]
        public void TransferMoney_FirstClientTransfersMoneyToTheSecondClientFromAnotherBank_FromFirstAccountMoneyAreWithdrawnTheSecondAccountIsReplenished()
        {
            _centralBank.TransferMoney(_firstClient.ReadableAccounts[0], _secondClient.ReadableAccounts[0], 30000F);
            Assert.IsTrue(_firstClient.ReadableAccounts[0].GetAmountOfMoney() == 70000F);
            Assert.IsTrue(_secondClient.ReadableAccounts[0].GetAmountOfMoney() == 80000F);
        }

        [Test]

        public void TransferMoney_ClientWithDoubtfulAccountTransfersMoreMoneyThanAllowed_CatchException()
        {
            _centralBank.GetBankByName("Sber").RegisterClient(_doubtfulClient);
            _centralBank.GetBankByName("Sber").CreateCreditAccount(_doubtfulClient, 15000F);
            Assert.Catch<DoubtfulAccountException>(() =>
            {
                _centralBank.TransferMoney(_doubtfulClient.ReadableAccounts[0], _firstClient.ReadableAccounts[0], 15000F);
            });
        }
        
        [Test]
        public void CancelTransaction_CentralBankCancelsDoubtfulTransferTransaction_FirstAccountIsReplenishedFromSecondAccountMoneyAreWithdrawn()
        {
            _centralBank.TransferMoney(_firstClient.ReadableAccounts[0], _secondClient.ReadableAccounts[0], 30000F);
            _centralBank.CancelTransaction(_centralBank.GetTransaction(1));
            Assert.IsTrue(_firstClient.ReadableAccounts[0].GetAmountOfMoney() == 100000F);
            Assert.IsTrue(_secondClient.ReadableAccounts[0].GetAmountOfMoney() == 50000F);
        }

        [Test]

        public void ScrollDays_ClientChecksWhatWillHappenWithTheirAccountInSeveralDays_DebitAndDepositAccountsHaveMoreMoney()
        {
            _centralBank.GetBankByName("Sber").CreateDepositAccount(_firstClient, 110000F);
            _centralBank.ScrollDays(30);
            Assert.IsTrue(_firstClient.ReadableAccounts[0].GetAmountOfMoney() == 100164.38F);
            Assert.IsTrue(_firstClient.ReadableAccounts[1].GetAmountOfMoney() == 110542.47F);
            Assert.IsTrue(_secondClient.ReadableAccounts[0].GetAmountOfMoney() == 50000F);
        }
    }
}