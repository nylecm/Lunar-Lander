using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class ProfileModel
{
    public int ID { get; set; } // TODO should this be made private
    public string Username { get; set; }
    public int HighScore { get; set; }
    public int NumberOfLandings { get; set; }
    public float[] LastLandingsVSpeeds { get; set; }
    public float[] LastScores { get; set; }
    public List<AchievementModel> AchievementsUnlocked { get; } = new List<AchievementModel>();

    public const int NumberOfLastLandingsVSpeeds = 10;
    public const int NumberOfLastScores = 10;

    public ProfileModel(int id, string username, int highScore, int numberOfLandings, float[] lastLandingsVSpeeds,
        float[] lastScores)
    {
        ID = id;
        Username = username;
        HighScore = highScore;
        NumberOfLandings = numberOfLandings;
        LastLandingsVSpeeds = lastLandingsVSpeeds;
        LastScores = lastScores;
    }
}