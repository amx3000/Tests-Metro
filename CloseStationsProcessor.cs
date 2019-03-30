using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Metro
{
    public class CloseStationsProcessor
    {
        public static CloseStationsProcessor Instance => new CloseStationsProcessor();

        private CloseStationsProcessor()
        {
        }

        public void Process(MetroSchema schema, string resultPath)
        {
            while (true)
            {
                if (schema.Stations.Count() == 0)
                    break;

                var stationToDelete = schema.Stations.FirstOrDefault(s => s.LinkedStations.Count() == 1);
                if (stationToDelete == null)
                    throw new InvalidOperationException("Невозможно в оставшейся схеме метро найти станцию, связанную только с одной другой станцией");

                AppendDeletedStationName(resultPath, stationToDelete.Name);
                schema.Delete(stationToDelete);
            }
        }

        private void AppendDeletedStationName(string resultPath, string stationName)
        {
            File.AppendAllLines(resultPath, new[] { stationName });
        }
    }
}