namespace csharp_commander.Commands
{
    public class MoveToPose : ICommand
    {
        public string CommandString => CommandStrings.MoveToPose;

        public MoveToPose(string project, string workState, double toolXMeters, double toolYMeters, double toolZMeters, double toolRotXRadians, double toolRotYRadians, double toolRotZRadians, double toleranceXMeters, double toleranceYMeters, double toleranceZMeters, double toleranceRotXRadians, double toleranceRotYRadians, double toleranceRotZRadians, bool completeMove, int completeMoveType, double speed)
        {
            this.Project = project;
            this.WorkState = workState;
            this.ToolXMeters = toolXMeters;
            this.ToolYMeters = toolYMeters;
            this.ToolZMeters = toolZMeters;
            this.ToolRotXRadians = toolRotXRadians;
            this.ToolRotYRadians = toolRotYRadians;
            this.ToolRotZRadians = toolRotZRadians;
            this.ToleranceXMeters = toleranceXMeters;
            this.ToleranceYMeters = toleranceYMeters;
            this.ToleranceZMeters = toleranceZMeters;
            this.ToleranceRotXRadians = toleranceRotXRadians;
            this.ToleranceRotYRadians = toleranceRotYRadians;
            this.ToleranceRotZRadians = toleranceRotZRadians;
            this.CompleteMove = completeMove;
            this.CompleteMoveType = completeMoveType;
            this.Speed = speed;

        }
        public string Project { get; set; }
        public string WorkState { get; set; }
        public double ToolXMeters { get; set; }
        public double ToolYMeters { get; set; }
        public double ToolZMeters { get; set; }
        public double ToolRotXRadians { get; set; }
        public double ToolRotYRadians { get; set; }
        public double ToolRotZRadians { get; set; }
        public double ToleranceXMeters { get; set; }
        public double ToleranceYMeters { get; set; }
        public double ToleranceZMeters { get; set; }
        public double ToleranceRotXRadians { get; set; }
        public double ToleranceRotYRadians { get; set; }
        public double ToleranceRotZRadians { get; set; }
        public bool CompleteMove { get; set; }
        public int CompleteMoveType { get; set; }
        public double Speed { get; set; }

        public override string ToString() => $"{CommandString},{WorkState},{ToolXMeters},{ToolYMeters},{ToolZMeters},{ToolRotXRadians},{ToolRotYRadians},{ToolRotZRadians},{ToleranceXMeters},{ToleranceYMeters},{ToleranceZMeters},{ToleranceRotXRadians},{ToleranceRotYRadians},{ToleranceRotZRadians},{(CompleteMove ? 1 : 0)},{CompleteMoveType},{Speed}";
    }
}