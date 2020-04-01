namespace csharp_commander.Commands
{
    public class ReleaseControl : ICommand
    {
        public string CommandString => CommandStrings.ReleaseControl;

        public ReleaseControl(string project, string workState)
        {
            this.Project = project;
            this.WorkState = workState;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState}";
    }
}