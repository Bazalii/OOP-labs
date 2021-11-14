using System;

namespace IsuExtra.Tools
{
    public class StudentFacultyGroupException : Exception
    {
        public StudentFacultyGroupException(string message)
            : base(message)
        {
        }
    }
}