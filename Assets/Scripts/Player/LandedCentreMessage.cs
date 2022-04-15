public struct LandedCentreMessage
{
    public LandedCentreMessage(string msg, int points, string sceneToLoad)
    {
        MSG = msg;
        Points = points;
        SceneToLoad = sceneToLoad;
    }

    public LandedCentreMessage(string msg, int points)
    {
        MSG = msg;
        Points = points;
        SceneToLoad = null;
    }

    public string MSG { get; }
    public double Points { get; }
    public string SceneToLoad { get; }

    public override string ToString() => $"({MSG}, {Points})";
}