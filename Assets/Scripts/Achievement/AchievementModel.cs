using System;

[Serializable]
public class AchievementModel
{
    public AchievementType AchievementType { get; }
    public string Name { get; }
    public string Description { get; }
    public int CurProgress { get; private set; } = 0;
    public int ProgressRequired { get; }

    public AchievementModel(AchievementType achievementType, string name, string description, int progressRequired)
    {
        AchievementType = achievementType;
        Name = name;
        Description = description;
        ProgressRequired = progressRequired;
    }

    public bool IsComplete()
    {
        return CurProgress >= ProgressRequired;
    }

    public void AddProgress()
    {
        CurProgress++;
    }
}