namespace csharp_commander.Commands
{
    public class AcquireControl : ICommand
    {
        public string CommandString => CommandStrings.AcquireControl;

        public AcquireControl(string project, string workState)
        {
            this.Project = project;
            this.WorkState = workState;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState}";
    }
}