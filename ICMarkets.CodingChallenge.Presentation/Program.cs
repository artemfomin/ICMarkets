using ICMarkets.CodingChallenge.Devices;
using ICMarkets.CodingChallenge.Devices.Connections;
using ICMarkets.CodingChallenge.Devices.ConsoleInterface;
using System;

namespace ICMarkets.CodingChallenge.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var cam = new Camera(ConnectionTypes.HTTP, new object(), "Backyard cam 1"))
            {
                cam.Connect();
                cam.RunCli();
            }

            Console.ReadKey();
        }
    }
}
