namespace csharp_commander.Commands
{
    public class EndOperationMode : ICommand
    {
        public string CommandString => CommandStrings.EndOperationMode;

        public override string ToString()
        {
            return $"{CommandString}";
        }
    }
}