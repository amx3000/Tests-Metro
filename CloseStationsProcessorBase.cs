using System;
using System.Linq;

namespace Metro
{
    public abstract class CloseStationsProcessorBase
    {
        public void Process(MetroSchema schema)
        {
            while (schema.Stations.Any())
            {
                // закрывать можно либо последнюю оставшуюся станцию, либо любую, связанную только с одной другой
                var stationToClose = schema.Stations.Count() == 1
                    ? schema.Stations.First()
                    : schema.Stations.FirstOrDefault(s => s.LinkedStations.Count() == 1);

                if (stationToClose == null)
                    throw new InvalidOperationException("Невозможно в оставшейся схеме метро найти станцию для закрытия");

                ProcessStationClose(stationToClose);

                schema.Close(stationToClose);
            }
        }

        protected abstract void ProcessStationClose(Station station);
    }
}