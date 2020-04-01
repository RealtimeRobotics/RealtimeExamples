using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_commander.Commands
{
    public interface ICommand
    {
        string CommandString { get; }

        string ToString();

    }
}
