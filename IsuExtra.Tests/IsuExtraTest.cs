﻿using System;
using System.Collections.Generic;
using IsuExtra.Entities;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class IsuExtraTest
    {
        private JointTrainingGroupService _jointTrainingGroupService = new ();
        
        [SetUp]
        public void Setup()
        {
            _jointTrainingGroupService = new JointTrainingGroupService();
            _jointTrainingGroupService.AddMegaFaculty("MTIT");
            _jointTrainingGroupService.AddMegaFaculty("FTM");
            
            _jointTrainingGroupService.AddStudyGroup("MTIT", "M3203");
            _jointTrainingGroupService.AddStudyGroup("MTIT", "M3204");
            

            _jointTrainingGroupService.GetStudyGroup("M3204")
                .AddLesson(new Lesson(new DateTime(2021, 10, 25, 8, 20, 00),
                    new DateTime(2021, 10, 25, 9, 50, 00),
                    240, "Moskalenko M.A."));
            _jointTrainingGroupService.GetStudyGroup("M3204")
                .AddLesson(new Lesson(new DateTime(2021, 10, 25, 10, 00, 00),
                    new DateTime(2021, 10, 25, 11, 30, 00),
                    466, "Moskalenko M.A."));
            _jointTrainingGroupService.AddStudent("M3204", "Ivan");
            
            _jointTrainingGroupService.AddStudyGroup("FTM", "R3301");
            _jointTrainingGroupService.GetStudyGroup("R3301")
                .AddLesson(new Lesson(new DateTime(2021, 10, 25, 8, 20, 00),
                    new DateTime(2021, 10, 25, 9, 50, 00), 
                    105, "Petrov I.L."));
            _jointTrainingGroupService.GetStudyGroup("R3301")
                .AddLesson(new Lesson(new DateTime(2021, 10, 28, 10, 00, 00),
                    new DateTime(2021, 10, 28, 11, 30, 00),
                    310, "Kutuzov M.U."));
            _jointTrainingGroupService.AddStudent("R3301", "Alexey");

            _jointTrainingGroupService.AddTrainingGroup("MTIT", "Discrete Math");
            _jointTrainingGroupService.GetTrainingGroup("Discrete Math").AddStream(new Stream("DM 1.1"));
            _jointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.1")
                .AddLesson(new Lesson(new DateTime(2021, 10, 28, 10, 00, 00),
                    new DateTime(2021, 10, 28, 11, 30, 00),
                    238, "Chuharev K.I."));
            _jointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.1")
                .AddLesson(new Lesson(new DateTime(2021, 10, 28, 15, 20, 00),
                    new DateTime(2021, 10, 28, 16, 50, 00),
                    466, "Chuharev K.I."));
            _jointTrainingGroupService.GetTrainingGroup("Discrete Math").AddStream(new Stream("DM 1.2"));
            _jointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.2")
                .AddLesson(new Lesson(new DateTime(2021, 10, 25, 8, 20, 00),
                    new DateTime(2021, 10, 25, 9, 50, 00),
                    238, "Chuharev K.I."));
            _jointTrainingGroupService.GetTrainingGroup("Discrete Math").GetStream("DM 1.2")
                .AddLesson(new Lesson(new DateTime(2021, 10, 28, 15, 20, 00),
                    new DateTime(2021, 10, 28, 16, 50, 00),
                    466, "Chuharev K.I."));
            
            _jointTrainingGroupService.AddTrainingGroup("FTM", "Photonics");
            _jointTrainingGroupService.GetTrainingGroup("Photonics").AddStream(new Stream("Ph 1.1"));
            _jointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1")
                .AddLesson(new Lesson(new DateTime(2021, 10, 25, 10, 00, 00),
                    new DateTime(2021, 10, 25, 11, 30, 00),
                    234, "Romanov M.D."));
            _jointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.1")
                .AddLesson(new Lesson(new DateTime(2021, 10, 27, 15, 20, 00),
                    new DateTime(2021, 10, 27, 16, 50, 00),
                    466, "Romanov M.D."));
            _jointTrainingGroupService.GetTrainingGroup("Photonics").AddStream(new Stream("Ph 1.2"));
            _jointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2")
                .AddLesson(new Lesson(new DateTime(2021, 10, 26, 11, 40, 00),
                    new DateTime(2021, 10, 26, 13, 10, 00),
                    238, "Romanov M.D."));
            _jointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2")
                .AddLesson(new Lesson(new DateTime(2021, 10, 28, 15, 20, 00),
                    new DateTime(2021, 10, 28, 16, 50, 00),
                    239, "Romanov M.D."));
        }

        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroup_JointTrainingGroupHasStudent()
        {
            _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(_jointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").GetStudent(1) != null);
        }
        
        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroupFromHisFaculty_ThrowException()
        {
            Assert.Catch<StudentFacultyGroupException>(() =>
            {
                _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Discrete Math");
            });
        }
        
        [Test]
        public void Enroll_StudentEnrollsToJointTrainingGroupWithCrossingLessons_ThrowException()
        {
            Assert.Catch<NoAvailableStreamsException>(() =>
            {
                _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(2), "Discrete Math");
            });
        }
        
        [Test]
        public void Enroll_StudentEnrollsToTheSameJointTraining_ThrowException()
        {
            _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.Catch<AlreadyEnrolledException>(() =>
            {
                _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Photonics");
            });
        }
        
        [Test]
        public void CancelEntry_StudentCancelsEntryToJointTrainingGroup_StudentIsNotInJointTrainingGroup()
        {
            _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Photonics");
            _jointTrainingGroupService.CancelEntry(_jointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(_jointTrainingGroupService.GetTrainingGroup("Photonics").GetStream("Ph 1.2").GetStudent(1) == null);
        }
        
        [Test]
        public void GetStreams_GetAllJointTrainingGroupStreams_ReceiveAllCorrespondingStreams()
        {
            IReadOnlyList<Stream> wantedStreams = _jointTrainingGroupService.GetStreams("Photonics");
            Assert.IsTrue(wantedStreams.Count == 2 && wantedStreams[0].Name == "Ph 1.1" && wantedStreams[1].Name == "Ph 1.2");
        }
        
        [Test]
        public void GetUnsignedStudents_GetAllStudentsThatAreNotInJointTrainingGroups_ReceiveAllCorrespondingStudents()
        {
            _jointTrainingGroupService.AddStudent("M3204", "Vladimir");
            _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(_jointTrainingGroupService.GetUnsignedStudents("M3204")[0].Name == "Vladimir" &&
                          _jointTrainingGroupService.GetUnsignedStudents("R3301")[0].Name == "Alexey");
        }

        [Test]
        public void GetStudents_GetAllStudentsThatAreEnrolledInStream_ReceiveAllCorrespondingStudents()
        {
            _jointTrainingGroupService.Enroll(_jointTrainingGroupService.GetStudent(1), "Photonics");
            Assert.IsTrue(_jointTrainingGroupService.GetStudents("Ph 1.2")[0].Name == "Ivan");
        }
    }
}