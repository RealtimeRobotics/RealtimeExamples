using System;

namespace csharp_commander.lib.Commands
{
    public class OffroadToHub : ICommand
    {
        public string CommandString => CommandStrings.OffroadToHub;
        public OffroadToHub(string project, string workState, string hubName, string pathQuality = "low", TimeSpan? timeout = null, bool moveNearObstacles = false)
        {
            this.Project = project;
            this.WorkState = workState;
            this.HubName = hubName;
            this.PathQuality = pathQuality;
            this.Timeout = timeout == null ? timeout : new TimeSpan(0, 0, 10);
            this.MoveNearObstacles = moveNearObstacles;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }
        public string HubName { get; set; }

        public string PathQuality { get; set; }
        public TimeSpan? Timeout { get; set; }
        public bool MoveNearObstacles { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState},{HubName},{PathQuality},{Timeout.Value.Seconds},{(MoveNearObstacles ? 1 : 0)}";

    }
}