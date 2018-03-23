using Newtonsoft.Json;
using System;

namespace MonopolyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var botje = new Botje("32ecb90722adf84b2e015425c36f78f2195601fe3378676c8384d0cebb4ae1e9");

            while (true)
            {
                //ConsoleKeyInfo key;
                //if (key != null && key.KeyChar == 's')
                //    break;
                botje.PerformNextStep();
                //Console.WriteLine(JsonConvert.SerializeObject(botje.GetStats()));
                Console.Write(".");
                //key = Console.ReadKey();
            }
        }
    }
}
