using System;

namespace csharp_commander.lib
{
    public class TcpDataEventArgs : EventArgs
    {
        public string Data { get; set; }

        public TcpDataEventArgs(string data)
        {
            Data = data;
        }
    }
}
