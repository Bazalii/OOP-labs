using System;

namespace IsuExtra
{
    public class StudentFacultyGroupException : Exception
    {
        public StudentFacultyGroupException(string message)
            : base(message)
        {
        }
    }
}