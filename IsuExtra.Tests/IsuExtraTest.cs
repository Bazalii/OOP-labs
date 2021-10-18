using System;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class IsuExtraTest
    {
        private JointTrainingGroupService JointTrainingGroupService = new ();
        
        [SetUp]
        public void Setup()
        {
            JointTrainingGroupService.AddMegaFaculty("FITIP");
            JointTrainingGroupService.AddMegaFaculty("FTM");
            
            JointTrainingGroupService.AddStudyGroup(JointTrainingGroupService.GetMegaFaculty("FITIP"), "M3204");
            JointTrainingGroupService.GetStudyGroup("M3204").AddLesson("Monday. 8:20-9:50", 240);
            JointTrainingGroupService.GetStudyGroup("M3204").AddLesson("Monday. 10:00-11:30", 466);
            JointTrainingGroupService.AddStudent(JointTrainingGroupService.GetStudyGroup("M3204"), "Ivan");

            JointTrainingGroupService.AddTrainingGroup(JointTrainingGroupService.GetMegaFaculty("FITIP"), "Discrete Math");
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").AddStream(new Stream("DM 1.1"));
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.1").AddLesson("Thursday. 10:00-11:30", 238);
            JointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.1").AddLesson("Thursday. 15:20-16:50", 466);

            JointTrainingGroupService.AddTrainingGroup(JointTrainingGroupService.GetMegaFaculty("FTM"), "Photonics");
            JointTrainingGroupService.GetTrainingGroup("Photonics").AddStream(new Stream("Ph 1.1"));
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1").AddLesson("Wednesday. 10:00-11:30", 100);
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1").AddLesson("Thursday. 15:20-16:50", 466);
            JointTrainingGroupService.GetTrainingGroup("Photonics").AddStream(new Stream("Ph 1.2"));
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").AddLesson("Tuesday. 10:00-11:30", 105);
            JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").AddLesson("Friday. 10:00-11:30", 329);
        }

        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroup_JointTrainingGroupHasStudent()
        {
            JointTrainingGroupService.Enroll(JointTrainingGroupService.GetStudent(1), "Photonics");
            Console.WriteLine(JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1").Students[0]);
            Assert.IsTrue(JointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1").GetStudent(1) != null);
        }
    }
}