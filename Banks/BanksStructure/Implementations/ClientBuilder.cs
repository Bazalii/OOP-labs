using Banks.Tools;

namespace Banks.BanksStructure.Implementations
{
    public class ClientBuilder
    {
        private Client _client = new ();
        internal ClientBuilder()
        {
        }

        public void Reset()
        {
            _client = new Client();
        }

        public void SetName(string name)
        {
            _client.SetName(name);
        }

        public void SetSurname(string surname)
        {
            _client.SetSurname(surname);
        }

        public void SetAddress(string address)
        {
            _client.SetAddress(address);
        }

        public void SetPassportNumber(string passportNumber)
        {
            _client.SetPassportNumber(passportNumber);
        }

        public Client GetResult()
        {
            if (_client.GetName() == null || _client.GetSurname() == null)
            {
                throw new ClientWithoutNecessaryField("Client should have name!");
            }

            Client client = _client;
            Reset();
            return client;
        }
    }
}