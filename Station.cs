using System;
using System.Collections.Generic;

namespace Metro
{
    public class Station
    {
        private static readonly HashSet<Station> linkedStations = new HashSet<Station>();

        public Station(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;
        }
        public string Name { get; private set; }

        public IEnumerable<Station> LinkedStations { get; } = linkedStations;

        public void Link(Station station) => linkedStations.Add(station);

        public void Unlink(Station station) => linkedStations.Remove(station);
    }
}
