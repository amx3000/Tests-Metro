using System;

namespace Metro
{
    class Program
    {
        static void Main(string[] args)
        {
            var schema = new MetroSchema();
            schema.Load("input.txt");

            var processor = new CloseStationsProcessor("output.txt");
            processor.Process(schema);

            Console.WriteLine("См. файл output.txt");
        }
    }
}
