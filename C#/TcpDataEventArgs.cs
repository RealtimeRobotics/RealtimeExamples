using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace csharp_commander
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
