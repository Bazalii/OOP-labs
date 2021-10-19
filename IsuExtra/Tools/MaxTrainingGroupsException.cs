using System;

namespace IsuExtra
{
    public class MaxTrainingGroupsException : Exception
    {
        public MaxTrainingGroupsException(string message)
            : base(message)
        {
        }
    }
}