namespace csharp_commander.lib.Commands
{
    public class CancelMove : ICommand
    {
        public string CommandString => CommandStrings.CancelMove;
        public CancelMove(string project, string workState)
        {
            this.Project = project;
            this.WorkState = workState;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState}";
    }
}