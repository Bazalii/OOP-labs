using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        private int _courseNumber;
        public CourseNumber(int inputCourseNumber)
        {
            if (inputCourseNumber > 4)
                throw new IsuException("Course should be a number in range of 1..4");

            _courseNumber = inputCourseNumber;
        }

        public List<Group> Groups { get; private set; } = new List<Group>();
    }
}