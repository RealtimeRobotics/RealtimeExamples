namespace csharp_commander.Commands
{
    public class ClearFaults : ICommand
    {
        public string CommandString => CommandStrings.ClearFaults;

        public ClearFaults()
        {

        }

        public override string ToString() => $"{CommandString}";
    }
}