using System;
using System.Collections.Generic;
using System.IO;

namespace Metro
{
    public class MetroSchema
    {
        private readonly Dictionary<string, Station> stations = new Dictionary<string, Station>();

        public MetroSchema()
        {
        }

        public IEnumerable<Station> Stations => stations.Values;

        public void Load(string path)
        {
            var lines = File.ReadAllLines(path);

            stations.Clear();
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var parts = lines[i].Split(' ');
                if (parts.Length != 2)
                    throw new InvalidOperationException($"Ошибка в строка {i} ({lines[i]}): ожидалось названия двух станций, разделенные пробелом");

                var station1 = GetOrAddStation(parts[0]);
                var station2 = GetOrAddStation(parts[1]);
                station1.Link(station2);
                station2.Link(station1);
            }
        }

        public void Delete(Station station)
        {
            stations.Remove(station.Name);
            foreach (var linkedStations in station.LinkedStations)
            {
                linkedStations.Unlink(station);
                station.Unlink(linkedStations);
            }
        }

        private Station GetOrAddStation(string stationName)
        {
            return stations.TryGetValue(stationName, out var station)
                ? station
                : stations[stationName] = new Station(stationName);
        }
    }
}
