namespace Banks.BanksStructure.Implementations
{
    public class Client
    {
        private string _name;

        private string _surname;

        private string _address;

        private int _passportNumber;

        public Client(string name, string surname, string address = null, int passportNumber = 0)
        {
            _name = name;
            _surname = surname;
            _address = address;
            _passportNumber = passportNumber;
        }

        internal Client()
        {
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
        }

        public string GetAddress()
        {
            return _address;
        }

        public void SetPassportNumber(int passportNumber)
        {
            _passportNumber = passportNumber;
        }

        public int GetPassportNumber()
        {
            return _passportNumber;
        }

        internal void SetName(string name)
        {
            _name = name;
        }

        internal void SetSurname(string surname)
        {
            _surname = surname;
        }
    }
}