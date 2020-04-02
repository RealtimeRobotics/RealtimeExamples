namespace csharp_commander.lib.Commands
{
    public class GetMode : ICommand
    {
        public string CommandString => CommandStrings.GetMode;

        public GetMode()
        {

        }

        public override string ToString()
        {
            return $"{CommandString}";
        }
    }
}