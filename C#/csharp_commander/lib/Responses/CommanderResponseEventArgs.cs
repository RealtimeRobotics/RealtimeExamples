using System;

namespace csharp_commander.lib.Responses
{
    public class CommanderResponseEventArgs : EventArgs
    {
        public Response Response { get; set; }

        public CommanderResponseEventArgs(Response resp)
        {
            Response = resp;
        }
    }
}