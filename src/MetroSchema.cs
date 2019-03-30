using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Metro
{
    public class MetroSchema
    {
        private readonly Dictionary<string, Station> stations = new Dictionary<string, Station>();

        public IEnumerable<Station> Stations => stations.Values;

        public void Load(string path)
        {
            var lines = File.ReadAllLines(path);

            stations.Clear();

            var n = 0;
            var m = 0;
            var l = lines.Length - 1;
            for (int i = 0; i <= l; i++)
            {
                var parts = lines[i].Split(' ');
                if (parts.Length != 2)
                    throw new InvalidOperationException($"Ошибка в строке {i} ({lines[i]}): ожидались названия двух станций, разделенные пробелом");

                if (i == 0)
                {
                    // читаем из первой строки числа N и M - число станций и число линий
                    n = int.Parse(parts[0]);
                    m = int.Parse(parts[1]);
                    continue;
                }

                var station1 = GetOrAddStation(parts[0]);
                var station2 = GetOrAddStation(parts[1]);
                if (station1 == station2)
                    throw new InvalidOperationException($"Ошибка в строке {i} ({lines[i]}): ожидались названия двух разных станций");

                station1.Link(station2);
                station2.Link(station1);
            }

            if (stations.Count != n)
                throw new InvalidOperationException($"Обещали {n} станций, а в файле - {n}");

            if (l != m)
                throw new InvalidOperationException($"Обещали {m} линий, а в файле - {l}");
        }

        public void Close(Station station)
        {
            stations.Remove(station.Name);
            foreach (var linkedStations in station.LinkedStations.ToArray())
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
