using Banks.BanksStructure.Implementations;

namespace Banks.ConsoleInterfaceStructure
{
    public class InputTransformer
    {
        public Client CreateClient(string name, string surname, string address, string passportNumber)
        {
            ClientBuilder clientBuilder = new ();
            clientBuilder.SetName(name);
            clientBuilder.SetSurname(surname);
            if (address != string.Empty)
            {
                clientBuilder.SetAddress(address);
            }

            if (passportNumber != string.Empty)
            {
                clientBuilder.SetPassportNumber(passportNumber);
            }

            return clientBuilder.GetResult();
        }
    }
}