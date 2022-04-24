using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private List<AchievementModel> _achievementProgress = new List<AchievementModel>();

    public ProfileModel(int id, int highScore, int numberOfLandings, List<AchievementModel> achievementsUnlocked)
    {
        ID = id;
        HighScore = highScore;
        NumberOfLandings = numberOfLandings;
    }

    public ProfileModel(int id)
    {
        // construct profile by loading from file
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
            _achievementProgress = profileFromFile._achievementProgress;
        }
        catch (Exception)
        {
            throw new ArgumentException("Profile " + id + " deserialization failure.");
        }
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

    public AchievementModel GetAchievementOfType(AchievementType achievementType)
    {
        for (int i = 0; i < _achievementProgress.Count; i++)
        {
            if (_achievementProgress[i].AchievementType == achievementType) return _achievementProgress[i];
        }

        return null;
    }

    public void AddAchievement(AchievementModel achievementModel)
    {
        for (int i = 0; i < _achievementProgress.Count; i++)
        {
            if (_achievementProgress[i].AchievementType == achievementModel.AchievementType)
                _achievementProgress.Remove(_achievementProgress[i]);
        }

        _achievementProgress.Add(achievementModel);
    }

    public int NumberOfAchievementsInProgress()
    {
        return _achievementProgress.Count;
    }

    public int NumberOfAchievementsUnlocked()
    {
        return _achievementProgress.Count(achievement => achievement.IsComplete());
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