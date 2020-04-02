namespace csharp_commander.lib.Commands
{
    public class MoveToHub : ICommand
    {
        public string CommandString => CommandStrings.MoveToHub;
        public MoveToHub(string project, string workState, string hubName, double speed)
        {
            this.Project = project;
            this.WorkState = workState;
            this.HubName = hubName;
            this.Speed = speed;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }
        public string HubName { get; set; }
        public double Speed { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState},{HubName},{Speed}";
    }
}