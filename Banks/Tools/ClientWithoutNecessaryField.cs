using System;

namespace Banks.Tools
{
    public class ClientWithoutNecessaryField : Exception
    {
        public ClientWithoutNecessaryField(string message)
            : base(message)
        {
        }
    }
}