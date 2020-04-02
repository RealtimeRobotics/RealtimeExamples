using System;
using csharp_commander.lib;
using csharp_commander.lib.Commands;
using csharp_commander.lib.Responses;
using System.Net;
using System.Threading;
using System.Threading.Tasks;


namespace csharp_commander.example
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {

            RtrCommander commander = new RtrCommander(IPAddress.Parse("127.0.0.1"), 9999);

            commander.OnResponseReceived += HandleResponse;

            ICommand getMode = new GetMode();

            for (int i = 0; i < 5; i++)
            {

                await commander.SendCommand(getMode);
                Thread.Sleep(1000);
            }

            Console.ReadKey();
        }

        static public void HandleResponse(object sender, CommanderResponseEventArgs eventArgs)
        {
            Console.WriteLine($"Response: {eventArgs.Response.ToString()}");
        }
    }
}
