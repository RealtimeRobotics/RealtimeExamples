namespace csharp_commander.lib.Commands
{
    public class ChangeWorkState : ICommand
    {
        public string CommandString => CommandStrings.ChangeWorkState;

        public ChangeWorkState(string project, string workState)
        {
            this.Project = project;
            this.WorkState = workState;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState}";
    }
}