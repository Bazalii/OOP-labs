using System;
using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra
{
    public class MegaFaculty
    {
        private readonly List<StudyGroup> _groups = new ();

        private readonly List<JointTrainingGroup> _trainingGroups = new ();

        public MegaFaculty(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
        }

        public string Name { get; }

        public IReadOnlyList<StudyGroup> Groups => _groups;

        public IReadOnlyList<JointTrainingGroup> TrainingGroups => _trainingGroups;

        public void AddTrainingGroup(string name)
        {
            _trainingGroups.Add(new JointTrainingGroup(name));
        }

        public void AddStudyGroup(string name)
        {
            _groups.Add(new StudyGroup(name));
        }
    }
}