namespace Banks.BanksStructure
{
    public interface IMyObserver
    {
        void Subscribe(IHandler handler);

        void Unsubscribe(string objectName);

        string Notify();
    }
}