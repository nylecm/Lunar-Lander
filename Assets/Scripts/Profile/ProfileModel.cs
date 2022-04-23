using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class ProfileModel
{
    public int ID { get; set; } // TODO should this be made private
    public int HighScore { get; set; }
    public int NumberOfLandings { get; set; }
    public float[] LastLandingsVSpeeds { get; set; }
    public int[] LastScores { get; set; }
    //public List<AchievementModel> AchievementsUnlocked { get; }

    public ProfileModel(int id, int highScore, int numberOfLandings, float[] lastLandingsVSpeeds,
        int[] lastScores, List<AchievementModel> achievementsUnlocked)
    {
        ID = id;
        HighScore = highScore;
        NumberOfLandings = numberOfLandings;
        LastLandingsVSpeeds = lastLandingsVSpeeds;
        LastScores = lastScores;
        //AchievementsUnlocked = achievementsUnlocked;
    }

    public ProfileModel(int id)
    {
        if (!DoesProfileExist(id))
            throw new ArgumentException("Profile " + id + " that you are trying to load does not exist.");

        var formatter = new BinaryFormatter();
        using var stream = new FileStream(GetSavePath(id), FileMode.Open);
        try
        {
            var profileFromFile = formatter.Deserialize(stream) as ProfileModel;
            Debug.Assert(profileFromFile.ID != null);
            ID = profileFromFile.ID;
            HighScore = profileFromFile.HighScore;
            NumberOfLandings = profileFromFile.NumberOfLandings;
            LastLandingsVSpeeds = profileFromFile.LastLandingsVSpeeds;
            LastScores = profileFromFile.LastScores;
            //AchievementsUnlocked = profileFromFile.AchievementsUnlocked;
        }
        catch (Exception)
        {
            throw new ArgumentException("Profile " + id + " deserialization failure.");
        }
        // construct by loading from file
    }

    public bool Save()
    {
        var formatter = new BinaryFormatter();

        using var stream = new FileStream(GetSavePath(ID), FileMode.Create);
        try
        {
            formatter.Serialize(stream, this);
        }
        catch (Exception)
        {
            return false;
        }

        return false;
    }
    
    public static ProfileModel Load(int id)
    {
        if (!DoesProfileExist(id))
            throw new ArgumentException("Profile " + id + " that you are trying to load does not exist.");

        var formatter = new BinaryFormatter();
        using var stream = new FileStream(GetSavePath(id), FileMode.Open);
        try
        {
            var profileFromFile = formatter.Deserialize(stream) as ProfileModel;
            Debug.Assert(profileFromFile.ID != null);
            return profileFromFile;
        }
        catch (Exception)
        {
            throw new ArgumentException("Profile " + id + " deserialization failure.");
        }
    }

    public bool Delete()
    {
        try
        {
            File.Delete(GetSavePath(ID));
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public static bool DoesProfileExist(int id)
    {
        return File.Exists(GetSavePath(id));
    }

    private static string GetSavePath(int id)
    {
        var dir = Path.Combine(Application.persistentDataPath, id + ".sav");
        return dir;
    }
}