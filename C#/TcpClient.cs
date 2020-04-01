using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace csharp_commander
{
    public class AsyncTcpClient
    {
        public int Port { get; }
        public IPAddress Address { get; }
        public bool Listening { get; private set; }
        public event EventHandler<TcpDataEventArgs> OnDataReceived;
        public event EventHandler OnConnected;
        private TcpClient _client;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;
        public AsyncTcpClient(IPAddress address, int port)
        {
            Port = port;
            Address = address;
        }

        public async Task StartAsync()
        {
            IPEndPoint remoteEP = new IPEndPoint(Address, Port);
            _client = new TcpClient(remoteEP);
            await _client.ConnectAsync(Address, Port);
            OnConnected?.Invoke(this, new EventArgs());
        }

        public async Task ListenAsync(CancellationToken? token = null)
        {
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token ?? new CancellationToken());
            _token = _tokenSource.Token;
            Listening = true;
            try
            {
                while (!_token.IsCancellationRequested)
                {
                    await Task.Run(async () =>
                    {
                        var stream = _client.GetStream();
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            var recv = await sr.ReadLineAsync();
                            OnDataReceived?.Invoke(this, new TcpDataEventArgs(recv));
                        }
                    }, _token);
                }
            }
            finally
            {
                Listening = false;
            }
        }

        public void Stop()
        {
            _tokenSource?.Cancel();
            _client.Close();
        }

        public async Task SendAsync(string message)
        {
            using (StreamWriter sw = new StreamWriter(_client.GetStream()))
            {
                await sw.WriteLineAsync(message);
                await sw.FlushAsync();
            }
        }
    }
}