﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.BanksStructure.Implementations
{
    public class Client : IMyObserver
    {
        private readonly List<Account> _accounts = new ();

        private readonly List<IMyDisposable> _subscriptionCancellations = new ();

        private string _name;

        private string _surname;

        private string _address;

        private string _passportNumber;

        public Client(string name, string surname, string address = null, string passportNumber = null)
        {
            _name = name ??
                    throw new ArgumentNullException(
                        nameof(name), "Name cannot be null!");
            _surname = surname ??
                       throw new ArgumentNullException(
                           nameof(surname), "Surname cannot be null!");
            _address = address;
            _passportNumber = passportNumber;
        }

        internal Client()
        {
        }

        public IReadOnlyList<Account> ReadableAccounts => _accounts;

        public void Subscribe(IHandler handler)
        {
            _subscriptionCancellations.Add(handler.Subscribe(this));
        }

        public void Unsubscribe(string objectName)
        {
            _subscriptionCancellations.FirstOrDefault(cancellation => cancellation.GetName() == objectName)?.Dispose();
        }

        public string Notify()
        {
            return GetName() + " is notified!";
        }

        public string GetName()
        {
            return _name;
        }

        public string GetSurname()
        {
            return _surname;
        }

        public void SetAddress(string address)
        {
            _address = address;
            ChangeAccountsDoubtfulness();
        }

        public string GetAddress()
        {
            return _address;
        }

        public void SetPassportNumber(string passportNumber)
        {
            _passportNumber = passportNumber;
            ChangeAccountsDoubtfulness();
        }

        public string GetPassportNumber()
        {
            return _passportNumber;
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public void RemoveAccount(Account account)
        {
            _accounts.Remove(account);
        }

        public List<string> GetAccountIds()
        {
            return _accounts.Select(account => account.GetId()).ToList();
        }

        internal void SetName(string name)
        {
            _name = name;
        }

        internal void SetSurname(string surname)
        {
            _surname = surname;
        }

        private void ChangeAccountsDoubtfulness()
        {
            foreach (Account account in _accounts)
            {
                account.SetDoubtfulness(false);
            }
        }
    }
}