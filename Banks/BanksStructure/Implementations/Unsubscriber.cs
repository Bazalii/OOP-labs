using System;
using System.Collections.Generic;

namespace Banks.BanksStructure.Implementations
{
    public class Unsubscriber : IMyDisposable
    {
        private string _name;

        private List<IMyObserver> _observers;

        private IMyObserver _observer;

        public Unsubscriber(string name, List<IMyObserver> observers, IMyObserver observer)
        {
            _name = name;
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }

        public string GetName()
        {
            return _name;
        }
    }
}