namespace csharp_commander.lib.Commands
{
    public class TerminateGroup : ICommand
    {
        public string CommandString => CommandStrings.TerminateGroup;

        public TerminateGroup(string deconflictionGroup)
        {
            this.DeconflictionGroup = deconflictionGroup;

        }
        public string DeconflictionGroup { get; set; }

        public override string ToString() => $"{CommandString},{DeconflictionGroup}";
    }
}