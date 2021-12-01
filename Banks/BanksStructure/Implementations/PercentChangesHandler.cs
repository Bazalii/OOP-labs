using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.BanksStructure.Implementations
{
    public class PercentChangesHandler : IHandler
    {
        private string _bankName;

        private List<IMyObserver> _observers = new ();

        public PercentChangesHandler(string bankName)
        {
            _bankName = bankName;
        }

        public List<string> Notify()
        {
            return _observers.Select(observer => observer.Notify()).ToList();
        }

        public IMyDisposable Subscribe(IMyObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_bankName, _observers, observer);
        }

        public string GetBankName()
        {
            return _bankName;
        }
    }
}