
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using csharp_commander.Commands;
using csharp_commander.Responses;

namespace csharp_commander
{
    public class RtrCommander
    {

        AsyncTcpClient server;

        public RtrCommander(IPAddress address, int port)
        {
            server = new AsyncTcpClient(address, port);
        }

        /// <summary>
        /// Starts the TCP Server and connects the event handler to handle incoming messages.
        /// </summary>
        public async Task StartServer()
        {
            server.OnDataReceived += HandleTcpMessage;
            server.OnConnected += HandleClientConnected;

            Console.WriteLine("Starting TCP Server.");
            await server.StartAsync();
            await server.ListenAsync();

            Console.WriteLine("TCP Server running.");
        }

        /// <summary>
        /// Stops the TCP Server.
        /// </summary>
        public void StopServer()
        {
            server.OnDataReceived -= HandleTcpMessage;
            Console.WriteLine("Stopping TCP Server.");
            server.Stop();
            Console.WriteLine("TCP server stopped.");
        }

        /// <summary>
        /// Sends a command to the Realtime Controller
        /// </summary>
        /// <param name="command">The Command to send. </param>
        /// <returns></returns>
        public async void SendCommand(ICommand command)
        {
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
            Console.WriteLine("Received: " + recv);

            Response resp = new Response(recv);

            // Do something with resp here.
        }

        /// <summary>
        /// Send a string over TCP to the Realtime Controller
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns></returns>
        private async Task SendAsync(string message)
        {
            await server.SendAsync(message);
        }
    }
}