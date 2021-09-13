using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    [TestFixture]
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
            _isuService.AddGroup("M3201");
            _isuService.AddGroup("M3204");
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Ivan");
            Assert.IsTrue(_isuService.FindGroup("M3204").Students.Exists(student =>
                student.Id == 1 && student.Name == "Ivan" && student.CurrentGroup == "M3204"));
            Assert.IsTrue(_isuService.FindStudent("Ivan").CurrentGroup == "M3204");
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Alexey");
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Stepan");
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Maxim");
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Elena");
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Tatiana");
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddStudent(_isuService.FindGroup("M3204"), "Ksenia");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("R3207");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Ivan");
            _isuService.ChangeStudentGroup(_isuService.FindStudent("Ivan"), _isuService.FindGroup("M3201"));
        }
    }
}