namespace csharp_commander.lib.Commands
{
    public class BlindMove : ICommand
    {
        public string CommandString => CommandStrings.BlindMove;

        public BlindMove(string project, string workState, double toolXMeters, double toolYMeters, double toolZMeters, double toolRotXRadians, double toolRotYRadians, double toolRotZRadians, int moveType, double speed, bool ignoreAllCollisions = false)
        {
            this.Project = project;
            this.WorkState = workState;
            this.ToolXMeters = toolXMeters;
            this.ToolYMeters = toolYMeters;
            this.ToolZMeters = toolZMeters;
            this.ToolRotXRadians = toolRotXRadians;
            this.ToolRotYRadians = toolRotYRadians;
            this.ToolRotZRadians = toolRotZRadians;
            this.MoveType = moveType;
            this.Speed = speed;
            this.IgnoreAllCollisions = ignoreAllCollisions;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }
        public double ToolXMeters { get; set; }
        public double ToolYMeters { get; set; }
        public double ToolZMeters { get; set; }
        public double ToolRotXRadians { get; set; }
        public double ToolRotYRadians { get; set; }
        public double ToolRotZRadians { get; set; }
        public int MoveType { get; set; }
        public double Speed { get; set; }
        public bool IgnoreAllCollisions { get; set; }

        public override string ToString() => $"{CommandString},{Project},{WorkState},{ToolXMeters},{ToolYMeters},{ToolZMeters},{ToolRotXRadians},{ToolRotYRadians},{ToolRotZRadians},{MoveType},{Speed},{(IgnoreAllCollisions ? 1 : 0)}";
    }
}