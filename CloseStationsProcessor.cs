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
        }

        public string ResultPath { get; }

        public override void Process(MetroSchema schema)
        {
            File.Create(ResultPath).Dispose();

            base.Process(schema);
        }
        protected override void ProcessStationClose(Station station)
        {
            File.AppendAllLines(ResultPath, new[] { station.Name });
        }
    }
}