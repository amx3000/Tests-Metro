using System;

namespace Metro
{
    class Program
    {
        static void Main(string[] args)
        {
            var schema = new MetroSchema();
            schema.Load(@"C:\Users\amx\Projects\Metro\input.txt");

            var processor = CloseStationsProcessor.Instance;
            processor.Process(schema, @"C:\Users\amx\Projects\Metro\output.txt");

            Console.WriteLine("См. файл output.txt");
        }
    }
}
