using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Queue<AchievementModel> _achievementQ = new Queue<AchievementModel>();

    public void NotifyAchievementComplete(string id)
    {
        _achievementQ.Enqueue(new AchievementModel(id));
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("AchievementQueueCheck");
    }

    private void UnlockAchievement(AchievementModel achievement)
    {
        // todo Do the work to asynchronously unlock the achievement.
        Debug.Log("Achievement Unlocked: " + achievement.GetId());
    }

    private IEnumerator AchievementQueueCheck()
    {
        for (;;)
        {
            if (_achievementQ.Count > 0) UnlockAchievement(_achievementQ.Dequeue());
            yield return new WaitForSeconds(5f);
        }
    }
}
