namespace csharp_commander.lib.Commands
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
