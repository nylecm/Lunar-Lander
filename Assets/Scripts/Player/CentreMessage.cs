public struct CentreMessage
{
    public CentreMessage(string msg, int points, string sceneToLoad)
    {
        MSG = msg;
        Points = points;
        SceneToLoad = sceneToLoad;
    }

    public CentreMessage(string msg, int points)
    {
        MSG = msg;
        Points = points;
        SceneToLoad = null;
    }
    
    public CentreMessage(string msg)
    {
        MSG = msg;
        Points = 0;
        SceneToLoad = null;
    }

    public string MSG { get; }
    public double Points { get; }
    public string SceneToLoad { get; }

    public override string ToString()
    {
        return $"{nameof(MSG)}: {MSG}, {nameof(Points)}: {Points}, {nameof(SceneToLoad)}: {SceneToLoad}";
    }
}