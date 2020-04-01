using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace csharp_commander
{
    public class TcpStreamEventArgs : EventArgs
    {
        public NetworkStream Stream { get; set; }

        public TcpStreamEventArgs(NetworkStream data)
        {
            Stream = data;
        }
    }
}
