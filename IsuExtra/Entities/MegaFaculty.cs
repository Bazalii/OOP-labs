using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra
{
    public class MegaFaculty
    {
        private List<StudyGroup> _groups = new ();

        private List<StudyGroup> _trainingGroups = new ();

        public MegaFaculty()
        {
        }

        public IReadOnlyList<Group> Groups { get; set; }

        public IReadOnlyList<Group> TrainingGroups { get; set; }

        public void AddTrainingGroup(string name)
        {
            _groups.Add(new StudyGroup(name));
        }
    }
}