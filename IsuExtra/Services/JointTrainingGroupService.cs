using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Services
{
    public class JointTrainingGroupService
    {
        private List<JointTrainingGroup> _megaFaculties = new ();

        public JointTrainingGroupService()
        {
        }

        public void AddTrainingGroup(MegaFaculty megaFaculty, string name)
        {
            megaFaculty.AddTrainingGroup(name);
        }

        public void Enroll(Student student, string trainingGroupName)
        {
        }

        public void CancelEntry(Student student, string trainingGroupName)
        {
        }

        public List<Stream> GetStreams(string trainingGroupName)
        {
            return null;
        }

        public List<Student> GetStudents(Stream @stream)
        {
            return null;
        }

        public List<Student> GetUnsignedStudents(StudyGroup @group)
        {
            return null;
        }
    }
}