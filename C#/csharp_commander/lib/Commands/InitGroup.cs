namespace csharp_commander.lib.Commands
{
    public class InitGroup : ICommand
    {
        public string CommandString => CommandStrings.InitGroup;

        public InitGroup(string deconflictionGroup, string project, string workState)
        {
            this.DeconflictionGroup = deconflictionGroup;
            this.Project = project;
            this.WorkState = workState;

        }
        public string DeconflictionGroup { get; set; }
        public string Project { get; set; }
        public string WorkState { get; set; }

        public override string ToString()
        {
            return $"{CommandString},{DeconflictionGroup},{Project},{WorkState}";
        }

    }
}