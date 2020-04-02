using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace csharp_commander.lib
{
    public class AsyncTcpClient
    {
        public IPEndPoint Endpoint { get; set; }
        public bool Listening { get; private set; }
        public TimeSpan PollingPeriod { get; set; } = TimeSpan.FromMilliseconds(100);
        public event EventHandler<TcpDataEventArgs> OnDataReceived;
        public event EventHandler OnConnected;
        private TcpClient _client;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private Timer _timer;

        /// <summary>
        /// Creates a new client that will connect to the given address and port.
        /// </summary>
        /// <param name="address">The IP Address of the Realtime Controller</param>
        /// <param name="port">The port to communicate over.</param>
        /// <returns></returns>
        public AsyncTcpClient(IPAddress address, int port) : this(new IPEndPoint(address, port)) { }

        /// <summary>
        /// Creates a new client that will connect to the given endpoint.
        /// </summary>
        /// <param name="ipEndPoint">The endpoint to connect to.</param>
        public AsyncTcpClient(IPEndPoint ipEndPoint)
        {
            Endpoint = ipEndPoint;
        }

        /// <summary>
        /// Creates a new client that will connec to the given address and port
        ///  and check for new messages at the given polling interval.
        /// </summary>
        /// <param name="address">The IP Address of the Realtime Controller</param>
        /// <param name="port">The port to communicate over.</param>
        /// <param name="pollingPeriod">The period at which to check for new messages on the TCP buffer.</param>
        /// <returns></returns>
        public AsyncTcpClient(IPAddress address, int port, TimeSpan pollingPeriod) : this(address, port)
        {
            PollingPeriod = pollingPeriod;
        }

        /// <summary>
        /// Creates a new client that will connec to the given endpoint
        ///  and check for new messages at the given polling interval.
        /// </summary>
        /// <param name="ipEndPoint">The endpoint to connect to.</param>
        /// <param name="pollingPeriod">The period at which to check for new messages on the TCP buffer.</param>
        /// <returns></returns>
        public AsyncTcpClient(IPEndPoint ipEndPoint, TimeSpan pollingPeriod) : this(ipEndPoint)
        {
            PollingPeriod = pollingPeriod;
        }

        /// <summary>
        /// Connect to the endpoint.
        /// </summary>
        public void Connect()
        {
            _client = new TcpClient();
            _client.Connect(Endpoint);

            if (_client.Connected)
            {
                OnConnected?.Invoke(this, new EventArgs());
                _streamWriter = new StreamWriter(_client.GetStream());
                _streamWriter.NewLine = "\r\n";
                _streamWriter.AutoFlush = true;
                _streamReader = new StreamReader(_client.GetStream());
            }
        }

        /// <summary>
        /// Start a thread that checks for new messages at the given polling interval
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        public void StartListening(CancellationToken? token = null)
        {
            _token = CancellationTokenSource.CreateLinkedTokenSource(token ?? new CancellationToken()).Token;

            var autoEvent = new AutoResetEvent(false);
            _timer = new Timer(CheckStream, autoEvent, 0, PollingPeriod.Milliseconds);

            Listening = true;
            Console.WriteLine("Listening...");
        }

        /// <summary>
        /// Callback that checks the stream for incoming data.
        /// </summary>
        /// <param name="stateInfo"></param>
        private void CheckStream(Object stateInfo)
        {

            if (!_client.Connected || _token.IsCancellationRequested)
            {
                Listening = false;
                return;
            }
            var recv = _streamReader.ReadLine();
            if (recv != null)
            {
                OnDataReceived?.Invoke(this, new TcpDataEventArgs(recv));
            }
        }

        /// <summary>
        /// Stop the client and disconnect.
        /// </summary>
        public void Stop()
        {
            _tokenSource?.Cancel();
            _timer.Dispose();
            _streamReader.Dispose();
            _streamWriter.Dispose();
            _client.Close();
        }

        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns></returns>
        public async Task SendAsync(string message)
        {
            await _streamWriter.WriteLineAsync(message);
        }

    }
}