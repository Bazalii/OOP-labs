using System.Collections.Generic;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class IsuExtraTest
    {
        private JointTrainingGroupService JointTrainingGroupService = new ();
        
        [OneTimeSetUp]
        public void Setup()
        {
            JointTrainingGroupService.AddMegaFaculty("FITIP");
            JointTrainingGroupService.AddMegaFaculty("FTM");
            
            JointTrainingGroupService.AddStudyGroup(JointTrainingGroupService.GetMegaFaculty("FITIP"), "M3204");
            JointTrainingGroupService.GetStudyGroup("M3204").AddLesson("Monday. 8:20-9:50", 240);
            JointTrainingGroupService.GetStudyGroup("M3204").AddLesson("Monday. 10:00-11:30", 466);
            JointTrainingGroupService.AddStudent(JointTrainingGroupService.GetStudyGroup("M3204"), "Ivan");
            
            JointTrainingGroupService.AddStudyGroup(JointTrainingGroupService.GetMegaFaculty("FTM"), "R3301");
            JointTrainingGroupService.GetStudyGroup("R3301").AddLesson("Monday. 8:20-9:50", 105);
            JointTrainingGroupService.GetStudyGroup("R3301").AddLesson("Thursday. 10:00-11:30", 310);
            JointTrainingGroupService.AddStudent(JointTrainingGroupService.GetStudyGroup("R3301"), "Alexey");

            JointTrainingGroupService.AddTrainingGroup(JointTrainingGroupService.GetMegaFaculty("FITIP"), "Discrete Math");
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").AddStream(new Stream("DM 1.1"));
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.1").AddLesson("Thursday. 10:00-11:30", 238);
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.1").AddLesson("Thursday. 15:20-16:50", 466);
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").AddStream(new Stream("DM 1.2"));
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.2").AddLesson("Monday. 8:20-9:50", 238);
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.2").AddLesson("Thursday. 15:20-16:50", 466);
            
            JointTrainingGroupService.AddTrainingGroup(JointTrainingGroupService.GetMegaFaculty("FTM"), "Photonics");
            JointTrainingGroupService.GetTrainingGroup("Photonics").AddStream(new Stream("Ph 1.1"));
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1").AddLesson("Monday. 10:00-11:30", 238);
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1").AddLesson("Wednesday. 15:20-16:50", 466);
            JointTrainingGroupService.GetTrainingGroup("Photonics").AddStream(new Stream("Ph 1.2"));
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").AddLesson("Tuesday. 11:40-13:10", 238);
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").AddLesson("Thursday. 15:20-16:50", 466);
        }

        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroup_JointTrainingGroupHasStudent()
        {
            JointTrainingGroupService.Enroll(JointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").GetStudent(1) != null);
        }
        
        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroupFromHisFaculty_ThrowException()
        {
            Assert.Catch<StudentFacultyGroupException>(() =>
            {
                JointTrainingGroupService.Enroll(JointTrainingGroupService.GetStudent(1), "Discrete Math");
            });
        }
        
        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroupWithCrossingLessons_ThrowException()
        {
            Assert.Catch<NoAvailableStreamsException>(() =>
            {
                JointTrainingGroupService.Enroll(JointTrainingGroupService.GetStudent(2), "Discrete Math");
            });
        }
        
        [Test]
        public void CancelEntry_StudentCancelsEntryToJointTrainingGroup_StudentIsNotInJointTrainingGroup()
        {
            JointTrainingGroupService.Enroll(JointTrainingGroupService.GetStudent(1), "Photonics");
            JointTrainingGroupService.CancelEntry(JointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").GetStudent(1) == null);
        }
        
        [Test]
        public void GetStreams_GetAllJointTrainingGroupStreams_ReceiveAllCorrespondingStreams()
        {
            IReadOnlyList<Stream> wantedStreams = JointTrainingGroupService.GetStreams("Photonics");
            Assert.IsTrue(wantedStreams.Count == 2 && wantedStreams[0].Name == "Ph 1.1" && wantedStreams[1].Name == "Ph 1.2");
        }
        
        [Test]
        public void GetUnsignedStudents_GetAllStudentsThatAreNotInJointTrainingGroups_ReceiveAllCorrespondingStreams()
        {
            JointTrainingGroupService.AddStudent(JointTrainingGroupService.GetStudyGroup("M3204"), "Vladimir");
            JointTrainingGroupService.Enroll(JointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(JointTrainingGroupService.GetUnsignedStudents("M3204")[0].Name == "Vladimir" &&
                          JointTrainingGroupService.GetUnsignedStudents("R3301")[0].Name == "Alexey");
        }
    }
}