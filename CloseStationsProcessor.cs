using System;
using System.IO;
using System.Text;

namespace Metro
{
    public class CloseStationsProcessor : CloseStationsProcessorBase
    {
        public CloseStationsProcessor(string resultPath)
        {
            
            if (string.IsNullOrWhiteSpace(resultPath))
                throw new ArgumentException("Недопустимое имя файла", nameof(resultPath));

            ResultPath = resultPath;
            
            File.Delete(ResultPath);
        }

        public string ResultPath { get; }

        protected override void ProcessStationClose(Station station)
        {
            File.AppendAllLines(ResultPath, new[] { station.Name });
        }
    }
}