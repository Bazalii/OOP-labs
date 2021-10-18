using System;
using System.Collections.Generic;
using System.Linq;

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

        public IReadOnlyList<Stream> Streams => _streams;

        public void AddStream(Stream stream)
        {
            _streams.Add(stream);
        }

        public Stream GetStream(string name)
        {
            return _streams.FirstOrDefault(stream => stream.Name == name);
        }
    }
}