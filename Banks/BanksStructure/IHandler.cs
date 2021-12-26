using System.Collections.Generic;

namespace Banks.BanksStructure
{
    public interface IHandler
    {
        List<string> Notify();

        IMyDisposable Subscribe(IMyObserver observer);
    }
}