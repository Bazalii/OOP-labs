using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        private int courseNumber = 0;

        public CourseNumber(int inputCourseNumber)
        {
            if (inputCourseNumber > 4)
                throw new IsuException("Course should be a number in range of 1..4");

            this.courseNumber = inputCourseNumber;
        }
    }
}