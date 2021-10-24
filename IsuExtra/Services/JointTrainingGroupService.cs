using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class JointTrainingGroupService
    {
        private readonly List<MegaFaculty> _megaFaculties = new ();

        private readonly List<Student> _unsignedStudents = new ();

        private int _studentIds;

        public void AddTrainingGroup(string megaFacultyName, string name)
        {
            _megaFaculties.Find(megaFaculty => megaFaculty.Name == megaFacultyName)?.AddTrainingGroup(name);
        }

        public void AddStudyGroup(string megaFacultyName, string name)
        {
            _megaFaculties.Find(megaFaculty => megaFaculty.Name == megaFacultyName)?.AddStudyGroup(name);
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
            if (CountStudentJointTrainingGroups(student) == 2)
            {
                throw new MaxTrainingGroupsException($"{student.Name} has already enrolled in two training groups");
            }

            if (CheckIfAlreadyEnrolled(student, wantedTrainingGroup))
            {
                throw new AlreadyEnrolledException($"{student.Name} is already enrolled in {trainingGroupName}");
            }

            Stream availableStream =
                FindAvailableStream(wantedTrainingGroup, student) ??
                throw new NoAvailableStreamsException($"There are no streams that {student.Name} can enroll in");
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
                if (stream.GetStudent(student.Id) == null) continue;
                stream.RemoveStudent(student.Id);
                if (CountStudentJointTrainingGroups(student) == 1)
                {
                    _unsignedStudents.Add(student);
                }
            }
        }

        public IReadOnlyList<Stream> GetStreams(string trainingGroupName)
        {
            return GetTrainingGroup(trainingGroupName).Streams;
        }

        public IReadOnlyList<Student> GetStudents(string streamName)
        {
            return _megaFaculties
                .SelectMany(
                    megaFaculty => megaFaculty.TrainingGroups)
                .SelectMany(trainingGroup => trainingGroup.Streams)
                .Where(stream => stream.Name == streamName)
                .Select(stream => stream.Students).FirstOrDefault();
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

        public void AddStudent(string studyGroupName, string name)
        {
            StudyGroup wantedGroup = GetStudyGroup(studyGroupName);
            var newStudent = new Student(_studentIds += 1, name, studyGroupName);
            wantedGroup.AddStudent(newStudent);
            _unsignedStudents.Add(newStudent);
        }

        public Student GetStudent(int id)
        {
            return _megaFaculties
                .SelectMany(megaFaculty => megaFaculty.Groups, (_, group) => group.GetStudent(id))
                .FirstOrDefault(wantedStudent => wantedStudent != null) ??
                   throw new NotExistException($"Student with id:{id} doesn't exist");
        }

        public JointTrainingGroup GetTrainingGroup(string name)
        {
            JointTrainingGroup wantedTrainingGroup = _megaFaculties
                                                         .SelectMany(
                                                             megaFaculty => megaFaculty.TrainingGroups)
                                                         .FirstOrDefault(trainingGroup => trainingGroup.Name == name) ??
                                                     throw new NotExistException($"Training group with this {name} doesn't exist!");

            return wantedTrainingGroup;
        }

        public StudyGroup GetStudyGroup(string name)
        {
            StudyGroup wantedStudyGroup = _megaFaculties
                                              .SelectMany(
                                                  megaFaculty => megaFaculty.Groups)
                                              .FirstOrDefault(group => group.Name == name) ??
                                          throw new NotExistException($"Study group {name} doesn't exist!");

            return wantedStudyGroup;
        }

        private bool CheckIfAlreadyEnrolled(Student studentToCheck, JointTrainingGroup trainingGroupToCheck)
        {
            return _megaFaculties
                .SelectMany(megaFaculty => megaFaculty.TrainingGroups)
                .SelectMany(trainingGroup => trainingGroup.Streams)
                .SelectMany(stream => stream.Students).Any(student => student.Id == studentToCheck.Id);
        }

        private Stream FindAvailableStream(JointTrainingGroup trainingGroup, Student student)
        {
            StudyGroup studentGroup = GetStudyGroup(student.CurrentGroup);
            foreach (Stream stream in trainingGroup.Streams)
            {
                Stream goodStream = stream;
                foreach (Lesson streamLesson in stream.Timetable)
                {
                    foreach (Lesson studyGroupLesson in studentGroup.Timetable)
                    {
                        if (streamLesson.StartTime == studyGroupLesson.StartTime && streamLesson.EndTime == studyGroupLesson.EndTime)
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

        private int CountStudentJointTrainingGroups(Student student)
        {
            int numberOfJointTrainingGroups =
                _megaFaculties
                    .SelectMany(megaFaculty => megaFaculty.TrainingGroups)
                    .SelectMany(trainingGroup => trainingGroup.Streams)
                    .SelectMany(stream => stream.Students).Count(currentStudent => currentStudent.Id == student.Id);

            return numberOfJointTrainingGroups;
        }

        private bool CheckIfMegaFacultiesAreTheSame(Student student, string trainingGroupName)
        {
            foreach (MegaFaculty megaFaculty in _megaFaculties)
            {
                Group wantedGroup = megaFaculty.Groups.FirstOrDefault(studyGroup => studyGroup.Name == student.CurrentGroup);
                JointTrainingGroup wantedTrainingGroup = megaFaculty.TrainingGroups.FirstOrDefault(trainingGroup => trainingGroup.Name == trainingGroupName);

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