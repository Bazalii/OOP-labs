using System.Collections.Generic;
using System.Linq;
using Isu.Services;

namespace IsuExtra.Services
{
    public class JointTrainingGroupService
    {
        private readonly List<MegaFaculty> _megaFaculties = new ();

        private readonly List<Student> _unsignedStudents = new ();

        private int _studentIds;

        public void AddTrainingGroup(MegaFaculty megaFaculty, string name)
        {
            megaFaculty.AddTrainingGroup(name);
        }

        public void AddStudyGroup(MegaFaculty megaFaculty, string name)
        {
            megaFaculty.AddStudyGroup(name);
        }

        public void AddMegaFaculty(string name)
        {
            _megaFaculties.Add(new MegaFaculty(name));
        }

        public void Enroll(Student student, string trainingGroupName)
        {
            if (CheckIfMegaFacultiesAreTheSame(student, trainingGroupName))
            {
                throw new StudentFacultyGroupException(
                    $"{student.Name} cannot enroll to {trainingGroupName} because they belong to the same faculty");
            }

            JointTrainingGroup wantedTrainingGroup = GetTrainingGroup(trainingGroupName);
            Stream availableStream =
                FindAvailableStream(wantedTrainingGroup, student) ??
                throw new NoAvailableStreamsException($"There are no streams that {student.Name} can enroll in");
            if (CountStudentJointTrainingGroups(student) == 2)
            {
                throw new MaxTrainingGroupsException($"{student.Name} has already enrolled in two training groups");
            }

            availableStream.AddStudent(student);
            if (CountStudentJointTrainingGroups(student) == 1)
            {
                _unsignedStudents.Remove(student);
            }
        }

        public void CancelEntry(Student student, string trainingGroupName)
        {
            JointTrainingGroup wantedTrainingGroup = GetTrainingGroup(trainingGroupName);
            foreach (Stream stream in wantedTrainingGroup.Streams)
            {
                if (stream.GetStudent(student.Id) != null)
                {
                    stream.RemoveStudent(student.Id);
                    if (CountStudentJointTrainingGroups(student) == 1)
                    {
                        _unsignedStudents.Add(student);
                    }
                }
            }
        }

        public IReadOnlyList<Stream> GetStreams(string trainingGroupName)
        {
            return GetTrainingGroup(trainingGroupName).Streams;
        }

        public IReadOnlyList<Student> GetStudents(string streamName)
        {
            return (from megaFaculty in _megaFaculties
                from trainingGroup in megaFaculty.TrainingGroups
                from stream in trainingGroup.Streams
                where stream.Name == streamName
                select stream.Students).FirstOrDefault();
        }

        public List<Student> GetUnsignedStudents(string groupName)
        {
            return _unsignedStudents.FindAll(student => student.CurrentGroup == groupName).ToList();
        }

        public MegaFaculty GetMegaFaculty(string name)
        {
            MegaFaculty wantedMegaFaculty = _megaFaculties.Find(megaFaculty => megaFaculty.Name == name);
            if (wantedMegaFaculty == null)
            {
                throw new NotExistException($"MegaFaculty {name} doesn't exist!");
            }

            return wantedMegaFaculty;
        }

        public void AddStudent(StudyGroup @studyGroup, string name)
        {
            var newStudent = new Student(_studentIds += 1, name, studyGroup.Name);
            studyGroup.AddStudent(newStudent);
            _unsignedStudents.Add(newStudent);
        }

        public Student GetStudent(int id)
        {
            foreach (MegaFaculty megaFaculty in _megaFaculties)
            {
                foreach (StudyGroup group in megaFaculty.Groups)
                {
                    Student wantedStudent = group.GetStudent(id);
                    if (wantedStudent != null)
                    {
                        return wantedStudent;
                    }
                }
            }

            throw new NotExistException($"Student with id:{id} doesn't exist");
        }

        public JointTrainingGroup GetTrainingGroup(string name)
        {
            foreach (MegaFaculty megaFaculty in _megaFaculties)
            {
                foreach (JointTrainingGroup studyGroup in megaFaculty.TrainingGroups)
                {
                    if (studyGroup.Name == name)
                    {
                        return studyGroup;
                    }
                }
            }

            throw new NotExistException($"Training group with this {name} doesn't exist!");
        }

        public StudyGroup GetStudyGroup(string name)
        {
            foreach (MegaFaculty megaFaculty in _megaFaculties)
            {
                foreach (StudyGroup studyGroup in megaFaculty.Groups)
                {
                    if (studyGroup.Name == name)
                    {
                        return studyGroup;
                    }
                }
            }

            throw new NotExistException($"Study group {name} doesn't exist!");
        }

        public Stream FindAvailableStream(JointTrainingGroup @trainingGroup, Student student)
        {
            StudyGroup studentGroup = GetStudyGroup(student.CurrentGroup);
            Stream goodStream = null;
            foreach (Stream stream in trainingGroup.Streams)
            {
                goodStream = stream;
                foreach (Lesson streamLesson in stream.Timetable)
                {
                    foreach (Lesson studyGroupLesson in studentGroup.Timetable)
                    {
                        if (streamLesson.Time == studyGroupLesson.Time)
                        {
                            goodStream = null;
                        }
                    }
                }

                if (goodStream != null)
                {
                    return goodStream;
                }
            }

            return null;
        }

        public int CountStudentJointTrainingGroups(Student student)
        {
            int numberOfJointTrainingGroups =
                (from megaFaculty in _megaFaculties
                    from trainingGroup in megaFaculty.TrainingGroups
                    from stream in trainingGroup.Streams
                    from currentStudent in stream.Students
                    select currentStudent).Count(currentStudent => currentStudent.Id == student.Id);

            return numberOfJointTrainingGroups;
        }

        public bool CheckIfMegaFacultiesAreTheSame(Student student, string trainingGroupName)
        {
            Group wantedGroup = null;
            JointTrainingGroup wantedTrainingGroup = null;
            foreach (MegaFaculty megaFaculty in _megaFaculties)
            {
                foreach (Group group in megaFaculty.Groups)
                {
                    if (student.CurrentGroup == group.Name)
                    {
                        wantedGroup = group;
                    }
                }

                foreach (JointTrainingGroup trainingGroup in megaFaculty.TrainingGroups)
                {
                    if (trainingGroup.Name == trainingGroupName)
                    {
                        wantedTrainingGroup = trainingGroup;
                    }
                }

                if (wantedGroup != null && wantedTrainingGroup != null)
                {
                    return true;
                }

                if ((wantedGroup == null && wantedTrainingGroup != null) ||
                    (wantedGroup != null))
                {
                    return false;
                }
            }

            return false;
        }
    }
}