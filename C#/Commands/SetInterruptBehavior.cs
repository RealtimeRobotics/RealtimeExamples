using System;

namespace csharp_commander.Commands
{
    public class SetInterruptBehavior : ICommand
    {
        public string CommandString => CommandStrings.SetInterruptBehavior;

        public SetInterruptBehavior(string project, int replanAttempts, TimeSpan timeout)
        {
            this.Project = project;
            this.ReplanAttempts = replanAttempts;
            this.Timeout = timeout;

        }
        public string Project { get; set; }
        public int ReplanAttempts { get; set; }
        public TimeSpan Timeout { get; set; }

        public override string ToString() => $"{CommandString},{Project},{ReplanAttempts},{Timeout.Seconds:N0}";
    }
}