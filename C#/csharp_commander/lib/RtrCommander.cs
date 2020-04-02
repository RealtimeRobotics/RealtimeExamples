
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using csharp_commander.lib.Commands;
using csharp_commander.lib.Responses;

namespace csharp_commander.lib
{
    public class RtrCommander
    {

        AsyncTcpClient client;
        public event EventHandler<CommanderResponseEventArgs> OnResponseReceived;


        public RtrCommander(IPAddress address, int port) : this(new IPEndPoint(address, port)) { }

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="endPoint"></param>
        public RtrCommander(IPEndPoint endPoint)
        {
            client = new AsyncTcpClient(endPoint);
            client.OnDataReceived += HandleTcpMessage;
            client.OnConnected += HandleClientConnected;
            Console.WriteLine("Starting TCP Server.");
            client.Connect();
            client.StartListening();
        }

        /// <summary>
        /// Stops the TCP Server.
        /// </summary>
        public void StopServer()
        {
            client.OnDataReceived -= HandleTcpMessage;
            Console.WriteLine("Stopping TCP Server.");
            client.Stop();
            Console.WriteLine("TCP server stopped.");
        }

        /// <summary>
        /// Sends a command to the Realtime Controller
        /// </summary>
        /// <param name="command">The Command to send. </param>
        /// <returns></returns>
        public async Task SendCommand(ICommand command)
        {
            Console.WriteLine($"Sending: {command.ToString()}");
            await this.SendAsync(command.ToString());
        }

        /// <summary>
        /// Handle the event triggered when the client connects.
        /// </summary>
        /// <param name="tcpSender">The object triggering the event.</param>
        /// <param name="e">The EventArgs</param>
        private void HandleClientConnected(object tcpSender, EventArgs e)
        {
            Console.WriteLine("TCP client connected.");
        }

        /// <summary>
        /// Handle incoming TCP Messages from the Realtime Controller
        /// </summary>
        /// /// <param name="tcpSender">The object triggering the event.</param>
        /// <param name="evt">The EventArgs containint the message.</param>
        private void HandleTcpMessage(object tcpSender, TcpDataEventArgs evt)
        {
            string recv = evt.Data;
            Response resp = new Response(recv);
            OnResponseReceived?.Invoke(this, new CommanderResponseEventArgs(resp));
        }

        /// <summary>
        /// Send a string over TCP to the Realtime Controller
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns></returns>
        private async Task SendAsync(string message)
        {
            await client.SendAsync(message);
        }
    }
}