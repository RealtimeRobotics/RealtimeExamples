using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_commander.Commands
{
    public class BeginOperationMode : ICommand
    {
        public string CommandString => CommandStrings.BeginOperationMode;

        public BeginOperationMode()
        {
        }

        public override string ToString()
        {
            return $"{CommandString}";
        }
    }
}
