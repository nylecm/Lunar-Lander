public struct LandedCentreMessage
{
    public LandedCentreMessage(string msg, int points)
    {
        MSG = msg;
        Points = points;
    }

    public string MSG { get; }
    public double Points { get; }

    public override string ToString() => $"({MSG}, {Points})";
}
