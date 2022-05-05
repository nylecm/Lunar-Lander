using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementPanelController : MonoBehaviour
{
    [SerializeField] private AchievementType achievementType;
    
    private void OnEnable()
    {
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        AchievementModel thisAchievement = ProfileManager.CurProfile.GetAchievementOfType(achievementType);
        if (ProfileManager.CurProfile.GetAchievementOfType(achievementType) == null)
        {
            components[0].text = AchievementFactory.MakeAchievementOfType(achievementType).Name;
            components[1].text = "Locked";
        }
        else
        {
            components[0].text = thisAchievement.Name;
            if (thisAchievement.IsComplete())
                components[1].text = "Unlocked";
            else
                components[1].text = "Progress: " + thisAchievement.CurProgress + "/" + thisAchievement.ProgressRequired;
        }

    }
}