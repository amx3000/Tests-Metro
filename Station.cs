using System;
using System.Collections.Generic;

namespace Metro
{
    public class Station
    {
        private readonly HashSet<Station> linkedStations = new HashSet<Station>();

        public Station(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Недопустимое название станции", nameof(name));

            Name = name;
        }
        public string Name { get; }

        public IEnumerable<Station> LinkedStations => linkedStations;

        public void Link(Station station) => linkedStations.Add(station);

        public void Unlink(Station station) => linkedStations.Remove(station);
    }
}
