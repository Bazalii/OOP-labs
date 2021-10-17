using System;
using System.Collections.Generic;

namespace IsuExtra
{
    public class JointTrainingGroup
    {
        private List<Stream> _streams = new ();

        public JointTrainingGroup(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
        }

        public string Name { get; }

        public void AddStream(Stream stream)
        {
            _streams.Add(stream);
        }
    }
}