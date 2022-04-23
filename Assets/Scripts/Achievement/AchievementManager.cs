using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Queue<AchievementModel> _achievementQ = new Queue<AchievementModel>();

    /**
     * Method called when achievement progress is made.
     */
    public void NotifyAchievementProgress(AchievementType achievementType)
    {
        // cur profile has not made progress on this achievement:
        if (ProfileManager.CurProfile.GetAchievementOfType(achievementType) == null) 
        {
            AchievementModel achievement = AchievementFactory.MakeAchievementOfType(achievementType);
            achievement.AddProgress();
            _achievementQ.Enqueue(achievement);
        }
        else // cur profile has already made progress on this achievement:
        {
            AchievementModel achievement = ProfileManager.CurProfile.GetAchievementOfType(achievementType);
            achievement.AddProgress();
            ProfileManager.CurProfile.AddAchievement(achievement);
            _achievementQ.Enqueue(achievement);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("AchievementQueueCheck");
    }

    private void ProgressAchievement(AchievementModel achievement)
    {
        // if (ProfileManager.CurProfile.AchievementsUnlocked.Contains(achievement))
        // {
        //     ProfileManager.CurProfile.AchievementsUnlocked.Add(achievement);
        //     ProfileManager.CurProfile.Save();
        Debug.Log("Achievement Progress Made: Name:" + achievement.Name + " Progress: " + achievement.CurProgress + "/" + achievement.ProgressRequired);
        // }
        // todo Do the work to asynchronously unlock the achievement.
    }

    private IEnumerator AchievementQueueCheck()
    {
        for (;;)
        {
            if (_achievementQ.Count > 0) ProgressAchievement(_achievementQ.Dequeue());
            yield return new WaitForSeconds(5f);
        }
    }
}
