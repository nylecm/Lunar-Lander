using System;

public class AchievementFactory
{
    public static AchievementModel MakeAchievementOfType(AchievementType achievementType)
    {
        AchievementModel achievement = null;
        switch (achievementType)
        {
            case AchievementType.OutOfFuel:
                achievement = new AchievementModel(AchievementType.OutOfFuel, "Out of Fuel",
                    "You have run out of fuel for the first time!", 1);
                break;
            case AchievementType.Butter:
                achievement = new AchievementModel(AchievementType.Butter, "Butter!",
                    "You have landed softly for the first time!", 1);
                break;
            case AchievementType.FirstLanding:
                achievement = new AchievementModel(AchievementType.FirstLanding, "First Landing!",
                    "You have survived you first landing!", 1);
                break;
            case AchievementType.ThreeButtersInARow:
                achievement = new AchievementModel(AchievementType.ThreeButtersInARow, "Thrice Lucky!",
                    "You have landed softly three times!", 1);
                break;
            case AchievementType.FiveLandingsInOneGame:
                achievement = new AchievementModel(AchievementType.FiveLandingsInOneGame, "Five in One!",
                    "You have landed five times in a single game!", 1);
                break;
            case AchievementType.FirstCrash:
                achievement = new AchievementModel(AchievementType.FirstCrash, "First Crash!",
                    "You have crashed for the first time!", 1);
                break;
            case AchievementType.HighScoreOver200:
                achievement = new AchievementModel(AchievementType.HighScoreOver200, "High Score of 200!",
                    "You are getting good!", 1);
                break;
            case AchievementType.HighScoreOver500:
                achievement = new AchievementModel(AchievementType.HighScoreOver500, "High Score of 500!",
                    "You've got good!", 1);
                break;
            case AchievementType.CaveLanding:
                achievement = new AchievementModel(AchievementType.CaveLanding, "Cave Landing",
                    "Is this even possible!?", 1);
                break;
            case AchievementType.TenthLanding:
                achievement = new AchievementModel(AchievementType.TenthLanding, "Tenth Landing",
                    "You have landed 10 times in total!", 10);
                break;
            case AchievementType.HundredthLanding:
                achievement = new AchievementModel(AchievementType.HundredthLanding, "Hundredth Landing",
                    "You are a veteran of this game!", 100);
                break;
            case AchievementType.FiftiethLanding:
                achievement = new AchievementModel(AchievementType.FiftiethLanding, "Fiftieth Landing",
                    "You have landed 50 times in total!", 50);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(achievementType), achievementType, null);
        }

        return achievement;
    }

    //public AchievementModel 
}