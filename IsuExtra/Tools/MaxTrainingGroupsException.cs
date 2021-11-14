using System;

namespace IsuExtra.Tools
{
    public class MaxTrainingGroupsException : Exception
    {
        public MaxTrainingGroupsException(string message)
            : base(message)
        {
        }
    }
}